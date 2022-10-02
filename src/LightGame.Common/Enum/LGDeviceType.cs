namespace LightGame.Common
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum LGDeviceType : uint
    {
        Unkown = 0,
        iPad = 1,
        iPhone = 2,
        Mac = 3, // 1-20 苹果预留
        Android = 21, // 21-60 安卓预留
        Windows = 61 // 61 windows预留
    }
}
