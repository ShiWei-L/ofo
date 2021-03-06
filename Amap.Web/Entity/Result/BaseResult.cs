﻿using Newtonsoft.Json;
using Windows.Web.Http;

namespace Amap.Web.Entity.Result
{
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("infocode")]
        public int InfoCode { get; set; } = -1;

        [JsonProperty("info")]
        public string Info { get; set; }

        /// <summary>
        /// 网页源文本
        /// </summary>
        public string SourceHtml { get; set; }

        /// <summary>
        /// Http状态
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
