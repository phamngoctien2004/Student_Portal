using System.Text.Json.Serialization;

namespace Application.DTOs.Common
{
	public class BaseQueryDTO : BaseRequestDTO
	{
		public string? Keyword { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 20;
        // sort
        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";
        [JsonIgnore]
		public bool IsGetAll { get; set; } = false;
	}

	public class BaseQueryDTO<T> : BaseQueryDTO
	{
		public T? Query { get; set; }
	}
}
