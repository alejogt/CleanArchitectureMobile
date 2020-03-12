package crc64f4c4146db70a3605;


public class TransfersSecondActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("testviper.Droid.Domains.Transfers.View.TransfersSecondActivity, testviper.Droid", TransfersSecondActivity.class, __md_methods);
	}


	public TransfersSecondActivity ()
	{
		super ();
		if (getClass () == TransfersSecondActivity.class)
			mono.android.TypeManager.Activate ("testviper.Droid.Domains.Transfers.View.TransfersSecondActivity, testviper.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
