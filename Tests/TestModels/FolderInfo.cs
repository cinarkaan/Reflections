namespace ReflectionTask
{
    public class FolderInfo
    {
        [CustomSerialize("folderName")]
        public string Name { get; set;}

        public int NotSerializableProperty { get; set; }

        [CustomSerialize("folderInfoInnerEntity")]
        public FolderInfoInnerEntity InnerEntity { get; set; }
    }
}
