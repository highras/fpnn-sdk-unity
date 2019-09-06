using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_FPPackage {

    private FPPackage _pkg;

    [SetUp]
    public void SetUp() {

        this._pkg = new FPPackage();
    }

    [TearDown]
    public void TearDown() {}


    /**
     *  GetKeyCallback(FPData data)
     */
    [Test]
    public void Package_GetKeyCallback_EmptyData() {

        string key = this._pkg.GetKeyCallback(new FPData());
        Assert.AreEqual("FPNN_0", key);
    }

    [Test]
    public void Package_GetKeyCallback_NullData() {

        string key = this._pkg.GetKeyCallback(null);
        Assert.AreEqual("FPNN_0", key);
    }

    [Test]
    public void Package_GetKeyCallback_SimpleData() {

        FPData data = new FPData();
        data.SetSeq(11);
        string key = this._pkg.GetKeyCallback(data);
        Assert.AreEqual("FPNN_11", key);
    }


    /**
     *  IsHttp(FPData data)
     */
    [Test]
    public void Package_IsHttp_EmptyData() {

        bool res = this._pkg.IsHttp(new FPData());
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsHttp_NullData() {

        bool res = this._pkg.IsHttp(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsHttp_SimpleData() {

        FPData data = new FPData();
        data.SetMagic(FPConfig.HTTP_MAGIC);
        bool res = this._pkg.IsHttp(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsTcp(FPData data)
     */
    [Test]
    public void Package_IsTcp_EmptyData() {

        bool res = this._pkg.IsTcp(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_IsTcp_NullData() {

        bool res = this._pkg.IsTcp(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsTcp_SimpleData() {

        FPData data = new FPData();
        data.SetMagic(FPConfig.TCP_MAGIC);
        bool res = this._pkg.IsTcp(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsMsgPack(FPData data)
     */
    [Test]
    public void Package_IsMsgPack_EmptyData() {

        bool res = this._pkg.IsMsgPack(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_IsMsgPack_NullData() {

        bool res = this._pkg.IsMsgPack(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsMsgPack_SimpleData() {

        FPData data = new FPData();
        data.SetFlag(1);
        bool res = this._pkg.IsMsgPack(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsJson(FPData data)
     */
    [Test]
    public void Package_IsJson_EmptyData() {

        bool res = this._pkg.IsJson(new FPData());
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsJson_NullData() {

        bool res = this._pkg.IsJson(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsJson_SimpleData() {

        FPData data = new FPData();
        data.SetFlag(0);
        bool res = this._pkg.IsJson(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsOneWay(FPData data)
     */
    [Test]
    public void Package_IsOneWay_EmptyData() {

        bool res = this._pkg.IsOneWay(new FPData());
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsOneWay_NullData() {

        bool res = this._pkg.IsOneWay(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsOneWay_SimpleData() {

        FPData data = new FPData();
        data.SetMtype(0);
        bool res = this._pkg.IsOneWay(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsTwoWay(FPData data)
     */
    [Test]
    public void Package_IsTwoWay_EmptyData() {

        bool res = this._pkg.IsTwoWay(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_IsTwoWay_NullData() {

        bool res = this._pkg.IsTwoWay(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsTwoWay_SimpleData() {

        FPData data = new FPData();
        data.SetMtype(1);
        bool res = this._pkg.IsTwoWay(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsQuest(FPData data)
     */
    [Test]
    public void Package_IsQuest_EmptyData() {

        bool res = this._pkg.IsQuest(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_IsQuest_NullData() {

        bool res = this._pkg.IsQuest(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsQuest_SimpleData() {

        FPData data = new FPData();
        data.SetMtype(1);
        bool res = this._pkg.IsQuest(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsAnswer(FPData data)
     */
    [Test]
    public void Package_IsAnswer_EmptyData() {

        bool res = this._pkg.IsAnswer(new FPData());
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsAnswer_NullData() {

        bool res = this._pkg.IsAnswer(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsAnswer_SimpleData() {

        FPData data = new FPData();
        data.SetMtype(2);
        bool res = this._pkg.IsAnswer(data);
        Assert.IsTrue(res);
    }


    /**
     *  IsSupportPack(FPData data)
     */
    [Test]
    public void Package_IsSupportPack_EmptyData() {

        bool res = this._pkg.IsSupportPack(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_IsSupportPack_NullData() {

        bool res = this._pkg.IsSupportPack(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_IsSupportPack_SimpleData() {

        FPData data = new FPData();
        data.SetFlag(1);
        bool res = this._pkg.IsSupportPack(data);
        Assert.IsTrue(res);
    }


    /**
     *  CheckVersion(FPData data)
     */
    [Test]
    public void Package_CheckVersion_EmptyData() {

        bool res = this._pkg.CheckVersion(new FPData());
        Assert.IsTrue(res);
    }

    [Test]
    public void Package_CheckVersion_NullData() {

        bool res = this._pkg.CheckVersion(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_CheckVersion_OORData() {

        FPData data = new FPData();
        data.SetVersion(2);
        bool res = this._pkg.CheckVersion(data);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_CheckVersion_SimpleData() {

        FPData data = new FPData();
        data.SetVersion(0);
        bool res = this._pkg.CheckVersion(data);
        Assert.IsTrue(res);
    }


    /**
     *  PeekHead(byte[] bytes)
     */
    [Test]
    public void Package_PeekHead_NullBytes() {

        byte[] bytes = null;
        FPData data = this._pkg.PeekHead(bytes);
        Assert.IsNull(data);
    }

    [Test]
    public void Package_PeekHead_0Bytes() {

        FPData data = this._pkg.PeekHead(new byte[0]);
        Assert.IsNull(data);
    }

    [Test]
    public void Package_PeekHead_10Bytes() {

        FPData data = this._pkg.PeekHead(new byte[10]);
        Assert.IsNull(data);
    }

    [Test]
    public void Package_PeekHead_16Bytes() {

        FPData data = this._pkg.PeekHead(new byte[16]);
        Assert.IsNotNull(data);
    }


    /**
     *  DeCode(FPData data)
     */
    [Test]
    public void Package_DeCode_NullData() {

        bool res = this._pkg.DeCode(null);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_DeCode_EmptyData() {

        bool res = this._pkg.DeCode(new FPData());
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_DeCode_0BytesData() {

        FPData data = new FPData();
        data.Bytes = new byte[0];
        bool res = this._pkg.DeCode(data);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_DeCode_10BytesData() {

        FPData data = new FPData();
        data.Bytes = new byte[10];
        bool res = this._pkg.DeCode(data);
        Assert.IsFalse(res);
    }

    [Test]
    public void Package_DeCode_16BytesData() {

        FPData data = new FPData();
        data.Bytes = new byte[16];
        bool res = this._pkg.DeCode(data);
        Assert.IsTrue(res);
    }


    /**
     *  EnCode(FPData data)
     */
    [Test]
    public void Package_EnCode_NullData() {

        byte[] bytes = this._pkg.EnCode(null);
        Assert.IsNull(bytes);
    }

    [Test]
    public void Package_EnCode_EmptyData() {

        byte[] bytes = this._pkg.EnCode(new FPData());
        Assert.IsNotNull(bytes);
    }


    /**
     *  GetByteArrayRange(byte[] arr, int start, int end)
     */
    [Test]
    public void Package_GetByteArrayRange_NullBytes() {

        byte[] bytes = this._pkg.GetByteArrayRange(null, 0, 0);
        Assert.IsNull(bytes);
    }

    [Test]
    public void Package_GetByteArrayRange_EmptyBytes() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[0], 0, 0);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_0_0() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], 0, 0);
        Assert.AreEqual(1, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_2_1() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], 2, 1);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_Negative_1_2() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], -1, -2);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_Negative_2_1() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], -2, -1);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_OOR_21_31() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], 21, 31);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_OOR_11_31() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], 11, 31);
        Assert.AreEqual(0, bytes.Length);
    }

    [Test]
    public void Package_GetByteArrayRange_OOR_21_11() {

        byte[] bytes = this._pkg.GetByteArrayRange(new byte[20], 21, 11);
        Assert.AreEqual(0, bytes.Length);
    }
}
