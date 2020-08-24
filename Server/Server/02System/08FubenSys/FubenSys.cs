using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server._02System._08FubenSys {
    public class FubenSys {
        private static FubenSys instance = null;
        public static FubenSys Instance {
            get {
                if (instance == null) {
                    instance = new FubenSys();
                }
                return instance;
            }
        }
        private CacheSvc cacheSvc = null;
        private CfgSvc cfgSvc = null;

        public void Init() {
            cacheSvc = CacheSvc.Instance;
            cfgSvc = CfgSvc.Instance;
            PECommon.Log("FubenSys Init Done.");
        }

        public void ReqFBFight(MsgPack pack) {
            ReqFBFight data = pack.msg.reqFBFight;
            GameMsg msg = new GameMsg {
                cmd = (int)CMD.RspFBFight
            };

            PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
            int power = cfgSvc.GetMapCfg(data.fbid).power;
            if (pd.fuben<data.fbid) {
                msg.err = (int)ErrorCode.ClientDataError;
            } else if(pd.power<power) {
                msg.err = (int)ErrorCode.LackPower;
            } else {
                pd.power -= power;
                if (cacheSvc.UpdatePlayerData(pd.id, pd)) {
                    RspFBFight rspFight = new RspFBFight {
                        fbid = data.fbid,
                        power = pd.power,
                    };
                    msg.rspFBFight = rspFight;
                } else {
                    msg.err = (int)ErrorCode.UpdateDBError;
                }
            }
            pack.session.SendMsg(msg);
        }

    }
}
