using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Integration_FPCallback {

	private FPCallback _callback;
	private EventDelegate _eventDelegate;

	[SetUp]
	public void SetUp() {

		FPManager.Instance.Init();
		this._callback = new FPCallback();

		Integration_FPCallback self = this;
		this._eventDelegate = (evd) => {

			self._callback.OnSecond(evd.GetTimestamp());
		};
		FPManager.Instance.AddSecond(this._eventDelegate);
	}

	[TearDown]
	public void TearDown() {

		if (this._eventDelegate != null) {

			FPManager.Instance.RemoveSecond(this._eventDelegate);
			this._eventDelegate = null;
		}

		this._callback.RemoveCallback();
	}


	/**
	 *  FPData
	 */
	[UnityTest]
	public IEnumerator Callback_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Exec", new FPData());

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Add_Exec() {

		int count = 0;
		CallbackDelegate callback = (cbd) => {

            count++;
        };

		this._callback.AddCallback("Callback_Add_Add_Exec_1", callback, 1 * 1000);
		this._callback.AddCallback("Callback_Add_Add_Exec_2", callback, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Add_Exec_1", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		this._callback.ExecCallback("Callback_Add_Add_Exec_2", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Remove_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Remove_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.AddCallback("Callback_Add_Remove_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Remove_Add_Exec", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Exec_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Exec_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Exec_Add_Exec", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		this._callback.AddCallback("Callback_Add_Exec_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Exec_Add_Exec", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Timeout_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Timeout_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);

		this._callback.AddCallback("Callback_Add_Timeout_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Timeout_Add_Exec", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Exec_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Exec_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Exec_Exec", new FPData());
		this._callback.ExecCallback("Callback_Add_Exec_Exec", new FPData());

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Exec_Timeout() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Exec_Timeout", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Callback_Add_Exec_Timeout", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Remove_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Remove_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.ExecCallback("Callback_Add_Remove_Exec", new FPData());

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Remove_Timeout() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Remove_Timeout", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Callback_Add_Timeout_Exec() {

		int count = 0;
		this._callback.AddCallback("Callback_Add_Timeout_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);

		this._callback.ExecCallback("Callback_Add_Timeout_Exec", new FPData());
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}


	/**
	 *  Exception
	 */
	[UnityTest]
	public IEnumerator Exception_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Exec", new Exception("Exception_Add_Exec"));

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Add_Exec() {

		int count = 0;
		CallbackDelegate callback = (cbd) => {

            count++;
        };

		this._callback.AddCallback("Exception_Add_Add_Exec_1", callback, 1 * 1000);
		this._callback.AddCallback("Exception_Add_Add_Exec_2", callback, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Add_Exec_1", new Exception("Exception_Add_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		this._callback.ExecCallback("Exception_Add_Add_Exec_2", new Exception("Exception_Add_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Remove_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Remove_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.AddCallback("Exception_Add_Remove_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Remove_Add_Exec", new Exception("Exception_Add_Remove_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Exec_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Exec_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Exec_Add_Exec", new Exception("Exception_Add_Exec_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		this._callback.AddCallback("Exception_Add_Exec_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Exec_Add_Exec", new Exception("Exception_Add_Exec_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Timeout_Add_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Timeout_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);

		this._callback.AddCallback("Exception_Add_Timeout_Add_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Timeout_Add_Exec", new Exception("Exception_Add_Timeout_Add_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(2, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Exec_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Exec_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Exec_Exec", new Exception("Exception_Add_Exec_Exec"));
		this._callback.ExecCallback("Exception_Add_Exec_Exec", new Exception("Exception_Add_Exec_Exec"));

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Exec_Timeout() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Exec_Timeout", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Exception_Add_Exec_Timeout", new Exception("Exception_Add_Exec_Timeout"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Remove_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Remove_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.ExecCallback("Exception_Add_Remove_Exec", new Exception("Exception_Add_Remove_Exec"));

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Remove_Timeout() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Remove_Timeout", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Exception_Add_Timeout_Exec() {

		int count = 0;
		this._callback.AddCallback("Exception_Add_Timeout_Exec", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);

		this._callback.ExecCallback("Exception_Add_Timeout_Exec", new Exception("Exception_Add_Timeout_Exec"));
		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}
}
