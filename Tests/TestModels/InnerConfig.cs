namespace ReflectionTask
{
    public class InnerConfig
    {
        [CustomSerialize("subVersion")]
        public int SubVersion { get; set; }

        [CustomSerialize()]
        public string SubName { get; set; }

        [CustomSerialize("subPath")]
        public string SubPath { get; set; }
    }
}
