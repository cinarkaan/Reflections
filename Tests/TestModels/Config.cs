namespace ReflectionTask
{
    public class Config
    {
        [CustomSerialize("name")]
        public string Name { get; set; }

        [CustomSerialize("path")]
        public string Path { get; set; }

        [CustomSerialize()]
        public int version { get; set; }

        [CustomSerialize()]
        public float floatValue { get; set; }

        public string notSerializableProperty { get; set; }

        [CustomSerialize("subConfigFirst")]
        public InnerConfig InnerConfig1 { get; set; }

        [CustomSerialize("subConfigLast")]
        public InnerConfig InnerConfig2 { get; set; }

        [CustomSerialize("folderInfo")]
        public FolderInfo FolderInfo { get; set; }
    }
}
