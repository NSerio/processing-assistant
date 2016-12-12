using System;

namespace RA.CreateProcessingSet.Helpers
{
	public class Constants
	{
		public class MaxLengths
		{
			public const Int32 MAX_LENGTH_FIRST_NAME = 125;
			public const Int32 MAX_LENGTH_LAST_NAME = 125;
			public const Int32 MAX_LENGTH_FULL_NAME = 255;
		}
		public class Urls
		{
			public const String URL_TO_PROCESSING_SET = "/Relativity/Case/Mask/View.aspx?AppID={0}&ArtifactID={1}&ArtifactTypeID={2}";
		}
		public class Names
		{
			public static readonly String ApplicationName = "Create Processing Set Application";
		}

		public class Messages
		{
			public const string PROFILE_REQUIRED_MESSAGE = "Please select a processing profile";
			public const string NAME_REQUIRED_MESSAGE = "Please enter a processing set name";
			public const string SOURCE_PATH_REQUIRED_MESSAGE = "Please select a source path";
			public const string CUSTODIAN_LEVEL_REQUIRED_MESSAGE = "Please enter a custodian level";
			public const string CUSTODIAN_LEVEL_POSITIVE = "Custodian level must be greater than 0";
			public const string CUSTODIAN_LEVEL_WHOLE_NUMBER = "Custodian level must be a whole number";
		}

		public class Guids
		{
			public class Application
			{
				public static readonly Guid CreateProcessingSet = new Guid("5645DF5B-E2DB-4DCE-A05D-117238C0088B");
			}

			public class Tab
			{
				public static readonly Guid CreateProcessingSet = new Guid("A575E9EA-9DA7-4D15-A10B-3D04F59879B6");
			}

			public class ObjectType
			{
				public static readonly Guid Custodian = new Guid("D216472D-A1AA-4965-8B36-367D43D4E64C");
				public static readonly Guid ProcessingProfile = new Guid("4BE0A8E2-C236-4DAC-B8DF-E7944B84CEE5");
				public static readonly Guid ProcessingDataSource = new Guid("BC7F8480-C80D-4E2D-8638-1FC69E7DFFFB");
				public static readonly Guid ProcessingSet = new Guid("45B1F80D-C4E7-4A8D-A72A-ED9E21F89900");
			}

			public class Fields
			{
				public class Custodian
				{
					public static readonly Guid Name = new Guid("57928EF5-F29D-4137-A215-3A9ABF3E3F82");
					public static readonly Guid FirstName = new Guid("34EE9D29-44BD-4FC5-8FF1-4335A826A07D");
					public static readonly Guid LastName = new Guid("0B846E7A-6E05-4544-B5A8-AD78C49D0257");
					public static readonly Guid DocumentNumberingPrefix = new Guid("BAE96568-2B16-4707-8074-BE267F205D9C");
                    public static readonly Guid CustodianType = new Guid("E714B260-9CF4-4874-9C76-45E6F32D3CE7");

                }

				public class ProcessingSet
				{
					public static readonly Guid Name = new Guid("FC85B775-037C-47C1-BAC3-BC08F4E85D9F");
					public static readonly Guid RelatedProcessingProfile = new Guid("512EDD77-0753-4D10-9C9F-5FCE42FBDA7C");
					public static readonly Guid DiscoverStatus = new Guid("513DD373-661B-4EA5-9AC4-43BEA2F793EE");
					public static readonly Guid InventoryStatus = new Guid("D04EF741-C622-4993-9A53-2F2E54F4D1D7");
					public static readonly Guid PublishStatus = new Guid("E3343C3E-0FFA-4846-B4D3-CB1E5A37140C");
					public static readonly Guid EmailRecipients = new Guid("D6BA2063-2CB5-4ED2-B2C0-D4051B1F9020");
				}

				public class ProcessingDataSource
				{
					public static readonly Guid RelatedCustodian = new Guid("16D04F50-8202-49FC-8279-73EB4B5EB426");
					public static readonly Guid SourcePath = new Guid("322AC439-6ED5-4CE6-8EB3-FCBC2974274B");
					public static readonly Guid DestinationFolder = new Guid("05A0FBF7-50AD-41E0-B3B4-F36F4F47BCDF");
					public static readonly Guid TimeZone = new Guid("39C056A2-BD03-46FB-A7FC-08F0E4D9462F");
					public static readonly Guid OcrLanguages = new Guid("B8C6E41F-EC35-410D-A5B9-8F5A31B8D72C");
					public static readonly Guid DocumentNumberingPrefix = new Guid("AE61EF4D-E19E-45F1-BDF6-87DBF8629BAF");
					public static readonly Guid Status = new Guid("14D571EC-EF97-4B3E-958F-6ED3669CFEA9");
					public static readonly Guid RelatedProcessingDataSource = new Guid("C3B241EB-49D7-4D4F-86E2-3C16C871E033");
					public static readonly Guid Order = new Guid("C469E039-D203-4ADA-837C-E4A7E4B6C956");
				}

				public class ProcessingProfile
				{
					public static readonly Guid DefaultDestinationFolder = new Guid("E387C847-FEED-47C6-AD0C-C7A1BB406617");
					public static readonly Guid DefaultOcrLanguages = new Guid("5F300C10-4660-4A4D-BFA9-E32C9846C3FA");
					public static readonly Guid DefaultDocumentNumberingPrefix = new Guid("4FEAC495-C466-4A6E-BD78-26FA0240E47C");
					public static readonly Guid DefaultTimeZone = new Guid("8FE787D3-473C-4945-8846-F0D1DAE2D5A7");
					public static readonly Guid Name = new Guid("7161A505-54ED-4EB1-94FE-004BCDC3E988");
				}
			}

			public class Choices
			{
				public class ProcessingSet
				{
					public static readonly Guid DiscoverStatusNotStarted = new Guid("955E0B6D-8E50-4DFB-945D-5631FC97F40A");
					public static readonly Guid InventoryStatusNotStarted = new Guid("4BAB4CC3-A8A8-438C-A649-6FD4E0D111EB");
					public static readonly Guid PublishStatusNotStarted = new Guid("FC78EB19-B905-479C-9C3C-6FC49B95EA08");
                    public static readonly Guid CustodianTypeEntity = new Guid("53BE7B97-E3CD-4339-87AB-CD839C44EDEC");
                    public static readonly Guid CustodianTypePerson = new Guid("610C432F-A83E-4955-A2C1-FAB4230A3521");
                }
			}
		}
	}
}