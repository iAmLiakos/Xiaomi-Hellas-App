package md55280b62bb42226b96526222f7d343aef;


public class WebPageActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"";
		mono.android.Runtime.register ("XiaomiMIUIHellas.WebPageActivity, XiaomiMIUIHellas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WebPageActivity.class, __md_methods);
	}


	public WebPageActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebPageActivity.class)
			mono.android.TypeManager.Activate ("XiaomiMIUIHellas.WebPageActivity, XiaomiMIUIHellas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
