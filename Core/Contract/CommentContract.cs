using System.Runtime.Serialization;

namespace Core.Contract
{
    [DataContract]
    public class CommentEditContract
    {
        [DataMember]
        public string VideoID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string CreateDate { get; set; }
    }

    [DataContract]
    public class AddCommentContract : CommentEditContract
    {
        [DataMember]
        public string CommentID { get; set; }
    }
    public class UpdateCommentContract : CommentEditContract
    {
        [DataMember]
        public string CommentID { get; set; }
    }




}
