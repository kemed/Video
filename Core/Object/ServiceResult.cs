using System;
using System.Collections.Generic;

namespace Core.Object
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            this.BadRequest();
        }
        public int code { get; set; }
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string description { get; set; }

        public void SetMessage(string message)
        {
            this.message = message;
        }
        public void OK()
        {
            this.code = 200;
            this.succeeded = true;
            this.message = "All OK";
        }
        public void OK(string message)
        {
            OK();
            this.message = message;
        }
        public void BadRequest()
        {
            this.code = 400;
            this.succeeded = false;
            this.message = "Bad Request";
        }
        public void BadRequest(string message)
        {
            BadRequest();
            this.message = message;
        }
        public void UnAuthorized()
        {
            this.code = 401;
            this.succeeded = false;
            this.message = "Unauthorized";
        }
        public void UnAuthorized(string message)
        {
            UnAuthorized();
            this.message = message;
        }
        public void NotFound()
        {
            this.code = 404;
            this.succeeded = false;
            this.message = "Not Found";
        }
        public void NotFound(string message)
        {
            NotFound();
            this.message = message;
        }
        public void Error(string message, string description)
        {
            this.code = 500;
            this.succeeded = false;
            this.message = message;
            this.description = description;
        }
    }
    public class APIResult : ServiceResult
    {
        public string response { get; set; }
    }
    public class ObjectResult<T>
    {
        public ObjectResult()
        {
            this.status = new ServiceResult();
        }
        public T obj { get; set; }
        public ServiceResult status { get; set; }
    }
    public class ListResult<T>
    {
        public ListResult()
        {
            this.listObj = new List<T>();
            this.status = new ServiceResult();
        }
        public List<T> listObj { get; set; }
        public ServiceResult status { get; set; }
    }

    public class ListPageResult<T> : ListResult<T>
    {
        public int count { get; set; }
    }
}
