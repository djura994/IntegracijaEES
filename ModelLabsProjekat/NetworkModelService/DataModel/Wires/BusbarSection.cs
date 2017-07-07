using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using TelventDMS.Common.DMS.Common;
using TelventDMS.Services.NetworkModelService.DataModel.Core;
using TelventDMS.Services.NetworkModelService.Common.Interface;
using TelventDMS.Common.Components.Logger;
using TelventDMS.Services.NetworkModelService.DataModel.Topology;
using TelventDMS.Services.NetworkModelService.Common;
using TelventDMS.Services.NetworkModelService.DataModel.Catal;
using TelventDMS.Services.NetworkModelService.Common.Validation.ErrorDescriptors;
using TelventDMS.Common.Components.IO;
using TelventDMS.Common.Components.Serialization;

namespace TelventDMS.Services.NetworkModelService.DataModel.Wires
{
	[DataContract]
	[InvariantDataContract(0x43)]
	public class BusbarSection : Connector
	{		
		private long baseVoltage = 0;

		public BusbarSection(long localId, long globalId) : base(localId, globalId) 
		{
		}

		public BusbarSection()
		{
		}

		[DataMember]
		[InvariantDataMember((ushort)(((long)ModelCode.BUSBAR_VOLTAGE & (long)ModelCodeMask.MASK_ATTRIBUTE_INDEX) >> 8))]
		public long BaseVoltage
		{
			get
			{
				return baseVoltage;
			}

			set
			{
				baseVoltage = value;
			}
		}		

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				BusbarSection x = (BusbarSection)obj;
				return (x.BaseVoltage == this.BaseVoltage);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{	
				case ModelCode.BUSBAR_VOLTAGE:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{				
				case ModelCode.BUSBAR_VOLTAGE:
					prop.SetValue(baseVoltage);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{			
				case ModelCode.BUSBAR_VOLTAGE:
					baseVoltage = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		public override void UpdateProperty(Property propertyNew, out Property propertyOld)
		{
			switch (propertyNew.Id)
			{				
				case ModelCode.BUSBAR_VOLTAGE:
					propertyOld = new Property(propertyNew.Id);
					propertyOld.SetValue(baseVoltage);
					baseVoltage = propertyNew.AsReference();
					break;

				default:
					base.UpdateProperty(propertyNew, out propertyOld);
					break;
			}
		}		

		#endregion IAccess implementation

		#region IReference implementation

		public override List<IEntity> GetReferencedEntities(IFind modelRef, ModelCode referenceId)
		{
			switch (referenceId)
			{
				case ModelCode.BUSBAR_VOLTAGE:
					List<IEntity> referencedEntities = new List<IEntity>();
					if (baseVoltage > 0)
					{
						referencedEntities.Add(modelRef.GetEntityForLocalId(baseVoltage));
					}
					else
					{
						referencedEntities.Add(null);
					}

					return referencedEntities;

				default:
					return base.GetReferencedEntities(modelRef, referenceId);
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if(baseVoltage != 0 && ((GetTypeOfReference(ModelCode.BUSBAR_VOLTAGE) & refType) != 0))
			{
				references[ModelCode.BUSBAR_VOLTAGE] = new List<long>();
				references[ModelCode.BUSBAR_VOLTAGE].Add(baseVoltage);
			}

			base.GetReferences(references, refType);
		}

		public override void UpdateReference(ModelCode referenceId, long localIdOld, long localIdNew)
		{
			switch(referenceId)
			{
				case ModelCode.BASEVOLTAGE_BARS:
					if (baseVoltage == localIdOld)
					{
						baseVoltage = localIdNew;
					}
					else
					{
						DMSLogger.Log(DMSLogger.LogLevel.Warning, string.Format("Entity (GID = 0x{0:x16}, LID = 0x{1:x16}) doesn't contain reference 0x{2:x16}. Reference isn't updated.", this.GlobalId, this.LocalId, localIdOld));
					}

					break;
				default:
					base.UpdateReference(referenceId, localIdOld, localIdNew);
					break;
			}
		}

		public override bool CheckReference(ModelCode referenceId, long localId)
		{
			switch (referenceId)
			{
				case ModelCode.BASEVOLTAGE_BARS:
					if (baseVoltage == localId)
					{
						return true;
					}

					return false;
				default:
					return base.CheckReference(referenceId, localId);
			}
		}

		#endregion IReference implementation

		#region IRDF implementation

		public override void GenerateRDF(IFind modelRef, XmlTextWriter xmlWriter)
		{
			// base class
			base.GenerateRDF(modelRef, xmlWriter);

			// reference
			xmlWriter.WriteStartElement("cim:BusbarSection.baseVoltage");
			xmlWriter.WriteAttributeString("rdf:resource", "#0x" + (baseVoltage == 0 ? 0.ToString("x16") : (modelRef.GetEntityForLocalId(baseVoltage).GlobalId).ToString("x16")));
			xmlWriter.WriteEndElement();
		}

		public override void ParseRDF(XmlTextReader xmlReader)
		{
		}

		#endregion IRDF implementation


		public override IdentifiedObject CopyIdentifiedObject()
		{
			BusbarSection bs = (BusbarSection)base.CopyIdentifiedObject();	
			bs.BaseVoltage = this.BaseVoltage;

			return bs;
		}
	}
}
