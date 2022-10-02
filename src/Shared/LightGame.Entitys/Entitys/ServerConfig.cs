using System.ComponentModel.DataAnnotations;

namespace LightGame.Entity
{
    public class ServerConfig : BaseEntity
    {
        /// <summary>
        /// 服务器等级,根据玩家vip等级分配服务器
        /// </summary>
        [MaxLength(2)]
        public int ServerLevel { get; set; }

        [MaxLength(20)]
        public string LoginIP { get; set; }

        [MaxLength(6)]
        public ushort LoginPort { get; set; }

        [MaxLength(20)]
        public string ApiIP { get; set; }

        [MaxLength(6)]
        public ushort ApiPort { get; set; }

        [MaxLength(20)]
        public string GatewayIP { get; set; }

        [MaxLength(6)]
        public ushort GatewayPort { get; set; }
    }
}
