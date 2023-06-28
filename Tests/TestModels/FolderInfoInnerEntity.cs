namespace ReflectionTask
{
    public class FolderInfoInnerEntity
    {
        [CustomSerialize("innerField1")]
        public int InnerField1 { get; set; }

        [CustomSerialize("innerField2")]
        public string InnerField2 { get; set; }
    }
}
