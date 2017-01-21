using System;
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
		public string id { get; set; }
		public bool isReadOnly { get; set; }
		public string name { get; set; }
	}

	public class GroupRootObjectModel
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("value")]
		public List<GroupValueModel> GroupValueModelList { get; set; }
	}
}
