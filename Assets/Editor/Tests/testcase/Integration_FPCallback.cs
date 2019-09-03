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
	public IEnumerator Add_Exec_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Callback", new FPData());

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Exec_Exec_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Exec_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Exec_Callback", new FPData());
		this._callback.ExecCallback("Add_Exec_Exec_Callback", new FPData());

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Exec_Timeout_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Timeout_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Timeout_Callback", new FPData());
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Remove_Exec_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Remove_Exec_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.ExecCallback("Add_Remove_Exec_Callback", new FPData());

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Add_Timeout_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Timeout_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Timeout_Exec_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Timeout_Exec_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
		this._callback.ExecCallback("Add_Timeout_Exec_Callback", new FPData());
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Remove_Timeout_Callback() {

		int count = 0;
		this._callback.AddCallback("Add_Remove_Timeout_Callback", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}


	/**
	 *  Exception
	 */
	[UnityTest]
	public IEnumerator Add_Exec_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Exception", new Exception("Add_Exec_Exception"));

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Exec_Exec_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Exec_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Exec_Exception", new Exception("Add_Exec_Exec_Exception"));
		this._callback.ExecCallback("Add_Exec_Exec_Exception", new Exception("Add_Exec_Exec_Exception"));

		yield return new WaitForSeconds(0.5f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Exec_Timeout_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Exec_Timeout_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.ExecCallback("Add_Exec_Timeout_Exception", new Exception("Add_Exec_Timeout_Exception"));
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Remove_Exec_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Remove_Exec_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		this._callback.ExecCallback("Add_Remove_Exec_Exception", new Exception("Add_Remove_Exec_Exception"));

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Add_Timeout_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Timeout_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Timeout_Exec_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Timeout_Exec_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(1, count);
		this._callback.ExecCallback("Add_Timeout_Exec_Exception", new Exception("Add_Timeout_Exec_Exception"));
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Add_Remove_Timeout_Exception() {

		int count = 0;
		this._callback.AddCallback("Add_Remove_Timeout_Exception", (cbd) => {

			count++;
		}, 1 * 1000);

		this._callback.RemoveCallback();
		yield return new WaitForSeconds(2.0f);
		Assert.AreEqual(0, count);
	}
}
