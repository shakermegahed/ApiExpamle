
using Entity.DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class QuerySingleResult<T>
    {
        #region [Properties]
        public bool IsSucceeded { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
        public int TotalRecords { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool IsFirstPage { get; set; } = false;
        public bool IsLastPage { get; set; } = false;
        public int PageCount { get; set; } = 0;
        #endregion

        #region [ctor]
        public QuerySingleResult() { }


        public QuerySingleResult(T data)
        {
            this.Data = data;
        }

        public QuerySingleResult(String error)
        {
            this.Errors.Add(error);
            this.IsSucceeded = false;
        }
        #endregion
    }

    public class QueryMultipleResult<T, K>
    {
        #region [Properties]
        public bool IsSucceeded { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
        public int TotalRecords { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool IsFirstPage { get; set; } = false;
        public bool IsLastPage { get; set; } = false;
        public int PageCount { get; set; } = 0;
        #endregion

        #region [ctor]
        public QueryMultipleResult() { }

        public QueryMultipleResult(T data)
        {
            this.Data = data;
        }

        public QueryMultipleResult(T data, PagedQueryParameters parameters)
        {
            this.Data = data;
            this.PageNumber = parameters.PageNumber;
            this.PageSize = parameters.PageSize;
            if (data is IEnumerable)
            {
                var collection = data as IEnumerable<K>;
                if (collection != null && collection.Count() > 0)
                {
                    var firstItem = collection.FirstOrDefault();

                    if (firstItem != null)
                    {
                        // TODO: remove this when all the repo return BaseModel
                        if ((firstItem as BaseEntity) != null)
                        {
                            this.TotalRecords = (firstItem as BaseEntity).TotalCount;
                        }
                        else if ((firstItem as BaseModel) != null)
                        {
                            this.TotalRecords = (firstItem as BaseModel).TotalCount;
                        }
                        this.IsFirstPage = parameters.PageNumber == 1;
                        this.PageCount = (this.TotalRecords % parameters.PageSize) == 0 ? (this.TotalRecords / parameters.PageSize) : (this.TotalRecords / parameters.PageSize) + 1;
                        this.IsLastPage = parameters.PageNumber == this.PageCount;
                    }
                }
            }
        }

        public QueryMultipleResult(String error)
        {
            this.Errors.Add(error);
            this.IsSucceeded = false;
        }
        #endregion
    }
}
