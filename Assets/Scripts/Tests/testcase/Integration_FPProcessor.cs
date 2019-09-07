using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;

using com.fpnn;

public class Integration_FPProcessor {

	private class TestProcessor:FPProcessor.IProcessor {

        public int ServiceCount { get; set; }
        public int HasPushCount { get; set; }
        public int SecondCount { get; set; }

        public void Service(FPData data, AnswerDelegate answer) {
            ServiceCount++;

            if (answer != null) {

            	answer(new object(), false);
            }
        }

        public bool HasPushService(string name) {
            HasPushCount++;
            return false;
        }

        public void OnSecond(long timestamp) {
            SecondCount++;
        }
    }

    private FPProcessor _psr;

	[SetUp]
	public void SetUp() {

		this._psr = new FPProcessor();
	}

	[TearDown]
	public void TearDown() {

		this._psr.Destroy();
	}

	[UnityTest]
	public IEnumerator Processor_Set_Second() {

		TestProcessor tpsr = new TestProcessor();

		this._psr.SetProcessor(tpsr);
		this._psr.OnSecond(1567853702);

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(1, tpsr.SecondCount);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServicePing() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(1, tpsr.ServiceCount);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServiceOther() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("Processor_Set_ServiceOther");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(0, tpsr.ServiceCount);
		Assert.AreEqual(0, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServicePing_SetNull() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });
		this._psr.SetProcessor(null);

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(1, tpsr.ServiceCount);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServicePing_Set() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });
		this._psr.SetProcessor(new TestProcessor());

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(1, tpsr.ServiceCount);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServicePing_Delay_Set() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });

		yield return new WaitForSeconds(0.1f);
		this._psr.SetProcessor(new TestProcessor());
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(1, tpsr.ServiceCount);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_ServicePing_Delay_Destroy() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Service(data, (payload, exception) => {

            count++;
        });

		yield return new WaitForSeconds(0.1f);
		this._psr.Destroy();
		Assert.AreEqual(1, tpsr.HasPushCount);
		Assert.AreEqual(1, tpsr.ServiceCount);
		Assert.AreEqual(1, count);
	}

	[UnityTest]
	public IEnumerator Processor_Set_Destroy_ServicePing() {

		int count = 0;
		FPData data = new FPData();
		TestProcessor tpsr = new TestProcessor();

		data.SetMethod("ping");
		this._psr.SetProcessor(tpsr);
		this._psr.Destroy();
		this._psr.Service(data, (payload, exception) => {

            count++;
        });

		yield return new WaitForSeconds(0.1f);
		Assert.AreEqual(0, tpsr.HasPushCount);
		Assert.AreEqual(0, tpsr.ServiceCount);
		Assert.AreEqual(0, count);
	}
}
