using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PowerBISampleApp
{
	public class DataSetModel
	{
		public string Id { get; set; }

		public string Name { get; set; }
	}

	public class GroupValueModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("isReadOnly")]
		public bool IsReadOnly { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class GroupRootObjectModel
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("value")]
		public List<GroupValueModel> GroupValueModelList { get; set; }
	}

	public class GroupDashboardRootObjectModel
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("value")]
		public List<GroupDashboardModel> GroupValueModelList { get; set; }
	}

	public class GroupDashboardModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		[JsonProperty("isReadOnly")]
		public bool IsReadOnly { get; set; }

		[JsonProperty("embedUrl")]
		public string EmbedUrl { get; set; }
	}
}
