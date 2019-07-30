using System.Runtime.Serialization;

namespace Core.Contract
{
    [DataContract]
    public class VideoEditContract
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string VideoName { get; set; }
        [DataMember]
        public string VideoPath { get; set; }
        [DataMember]
        public string CreateDate { get; set; }
    }
    [DataContract]
    public class AddVideoContract : VideoEditContract
    {
        [DataMember]
        public string VideoID { get; set; }
    }
    public class UpdateVidioContract : VideoEditContract
    {
        [DataMember]
        public string VideoID { get; set; }
    }




}
