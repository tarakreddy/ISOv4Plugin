﻿using System;
using System.Collections.Generic;
using System.Linq;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using AgGateway.ADAPT.ApplicationDataModel.Representations;
using AgGateway.ADAPT.ISOv4Plugin.ObjectModel;
using AgGateway.ADAPT.Representation.RepresentationSystem;
using AgGateway.ADAPT.Representation.RepresentationSystem.ExtensionMethods;
using EnumeratedRepresentation = AgGateway.ADAPT.ApplicationDataModel.Representations.EnumeratedRepresentation;
using EnumerationMember = AgGateway.ADAPT.Representation.RepresentationSystem.EnumerationMember;

namespace AgGateway.ADAPT.ISOv4Plugin.ImportMappers.LogMappers
{
    public class CondensedWorkStateMeterCreator :  CondensedStateMeterCreator
    {
        public CondensedWorkStateMeterCreator(int ddi, int baseDdi)
        {
            DDI = ddi;
            StartingDdi = baseDdi;
            SectionValueToEnumerationMember = new Dictionary<int, EnumerationMember>
            {
                {0, DefinedTypeEnumerationInstanceList.dtiRecordingStatusOff},
                {1, DefinedTypeEnumerationInstanceList.dtiRecordingStatusOn},
                {2, DefinedTypeEnumerationInstanceList.dtiRecordingStatusOff},
                {3, DefinedTypeEnumerationInstanceList.dtiRecordingStatusOff}
            };
            Representation = RepresentationInstanceList.dtRecordingStatus.ToModelRepresentation();
        }

        public override UInt32 GetMetersValue(List<WorkingData> meters, SpatialRecord spatialRecord)
        {
            var returnValue = new UInt32();
            for (int i = 0; i < meters.Count; i++)
            {
                var sectionId = ((DDI - StartingDdi) * 16) + (i + 1);

                var meter = (ISOEnumeratedMeter)meters.SingleOrDefault(x => x.DeviceElementUseId == sectionId);
                var value = (EnumeratedValue)spatialRecord.GetMeterValue(meter);

                int meterInt;
                if (value.Value.Code == DefinedTypeEnumerationInstanceList.dtiRecordingStatusOff.ToModelEnumMember().Code)
                    meterInt = 0;
                else if (value.Value.Code == DefinedTypeEnumerationInstanceList.dtiRecordingStatusOn.ToModelEnumMember().Code)
                    meterInt = 1;
                else
                    meterInt = 3;

                returnValue |= Convert.ToUInt32(meterInt << (i * 2));
            }
            return returnValue;
        }
    }

    public class CondensedSectionOverrideStateMeterCreator : CondensedStateMeterCreator
    {
        public CondensedSectionOverrideStateMeterCreator(int ddi)
        {
            DDI = ddi;
            StartingDdi = 367;
            SectionValueToEnumerationMember = new Dictionary<int, EnumerationMember>
            {
                {0, DefinedTypeEnumerationInstanceList.dtiPrescriptionUsed},
                {1, DefinedTypeEnumerationInstanceList.dtiPrescriptionOverridden},
                {2, DefinedTypeEnumerationInstanceList.dtiPrescriptionUsed},
                {3, DefinedTypeEnumerationInstanceList.dtiPrescriptionNotUsed}
            };
            Representation = RepresentationInstanceList.dtPrescriptionState.ToModelRepresentation();
        }

        public override UInt32 GetMetersValue(List<WorkingData> meters, SpatialRecord spatialRecord)
        {
            var returnValue = new UInt32();
            for (int i = 0; i < meters.Count; i++)
            {
                var sectionId = ((DDI - StartingDdi) * 16) + (i + 1);

                var meter = (ISOEnumeratedMeter)meters.SingleOrDefault(x => x.DeviceElementUseId == sectionId);
                var value = (EnumeratedValue)spatialRecord.GetMeterValue(meter);

                int meterInt;
                if (value.Value.Code == DefinedTypeEnumerationInstanceList.dtiPrescriptionUsed.ToModelEnumMember().Code)
                    meterInt = 0;
                else if (value.Value.Code == DefinedTypeEnumerationInstanceList.dtiPrescriptionOverridden.ToModelEnumMember().Code)
                    meterInt = 1;
                else
                    meterInt = 3;

                returnValue |= Convert.ToUInt32(meterInt << (i * 2));
            }
            return returnValue;
        }
    }

    public abstract class CondensedStateMeterCreator : IEnumeratedMeterCreator
    {
     
        public int StartingDdi { get; set; }
        public int DDI { get; set; }
        public int StartingSection { get { return (DDI - StartingDdi) * 16 + 1; } }
        public ApplicationDataModel.Representations.Representation Representation { get; set; }

        public Dictionary<int, EnumerationMember> SectionValueToEnumerationMember { get; set; } 

        public List<ISOEnumeratedMeter> CreateMeters(IEnumerable<ISOSpatialRow> spatialRows)
        {
            var spatialRowWithDdi = spatialRows.FirstOrDefault(x => x.SpatialValues.Any(y => Convert.ToInt32(y.Dlv.A, 16) == DDI));

            int numberOfSections = 16;
            if (spatialRowWithDdi != null)
            {
                var spatialValue = spatialRowWithDdi.SpatialValues.First(x => Convert.ToInt32(x.Dlv.A, 16) == DDI);
                numberOfSections = GetNumberOfInstalledSections(spatialValue);
            }

            var meters = new List<ISOEnumeratedMeter>();
            for (int i = StartingSection; i < StartingSection + numberOfSections; i++)
            {
                meters.Add(new ISOEnumeratedMeter
                {
                    DeviceElementUseId = i,
                    Representation = Representation,
                    GetEnumeratedValue = GetValueForMeter
                });
            }

            return meters;
        }

        public EnumeratedValue GetValueForMeter(SpatialValue value, EnumeratedWorkingData meter)
        {
            var sectionValue = GetSectionValue((uint)value.Value, meter.DeviceElementUseId);
            var enumerationMember = SectionValueToEnumerationMember[(int)sectionValue];

            return new EnumeratedValue
            {
                Representation = meter.Representation as EnumeratedRepresentation,
                Value = enumerationMember.ToModelEnumMember(),
                Code = (int)enumerationMember.DomainTag
            };
        }

        public abstract UInt32 GetMetersValue(List<WorkingData> meters, SpatialRecord spatialRecord);

        private int GetNumberOfInstalledSections(SpatialValue spatialValue)
        {
            var value = (uint)spatialValue.Value;
            const uint notInstalledValue = 0x03;

            for (int i = 1; i < 17; i++)
            {
                if (GetSectionValue(value, i) == notInstalledValue)
                    return i - 1;
            }

            return 16;
        }

        private uint GetSectionValue(uint value, int section)
        {
            int zeroBasedSection = (section % 16) - 1;
            var sectionValue = value >> (zeroBasedSection * 2) & 0x03;
            return sectionValue;
        }
    }
}