/****************************************************
	文件：FubenSys.cs
	作者：SIKI学院——Plane
	邮箱: 1785275942@qq.com
	日期：2019/03/01 4:01   	
	功能：副本业务系统
*****************************************************/

public class FubenSys : SystemRoot {
    public static FubenSys Instance = null;

    public FubenWnd fubenWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        PECommon.Log("Init FubenSys...");
    }

    public void EnterFuben() {
        OpenFubenWnd();
    }

    #region Fuben Wnd
    public void OpenFubenWnd() {
        fubenWnd.SetWndState();
    }
    #endregion

}
