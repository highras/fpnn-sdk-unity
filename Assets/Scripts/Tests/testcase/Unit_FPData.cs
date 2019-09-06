using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_FPData {

	private FPData _data;

    [SetUp]
    public void SetUp() {

    	this._data = new FPData();
    }

    [TearDown]
    public void TearDown() {}


    /**
     *	Magic
     */
    [Test]
    public void Data_Magic_Default() {

        Assert.AreEqual(FPConfig.TCP_MAGIC, this._data.GetMagic());
    }

    [Test]
    public void Data_Magic_NullValue() {

    	this._data.SetMagic(FPConfig.TCP_MAGIC);
        this._data.SetMagic(null);
        Assert.AreEqual(FPConfig.TCP_MAGIC, this._data.GetMagic());
    }

    [Test]
    public void Data_Magic_EmptyValue() {

    	this._data.SetMagic(FPConfig.TCP_MAGIC);
        this._data.SetMagic(new byte[0]);
        Assert.AreEqual(FPConfig.TCP_MAGIC, this._data.GetMagic());
    }

    [Test]
    public void Data_Magic_SimpleValue() {

    	this._data.SetMagic(FPConfig.TCP_MAGIC);
        this._data.SetMagic(FPConfig.HTTP_MAGIC);
        Assert.AreEqual(FPConfig.HTTP_MAGIC, this._data.GetMagic());
    }


    /**
     *	Version
     */
    [Test]
    public void Data_Version_Default() {

        Assert.AreEqual(1, this._data.GetVersion());
    }

    [Test]
    public void Data_Version_ZeroValue() {

    	this._data.SetVersion(1);
    	this._data.SetVersion(0);
        Assert.AreEqual(0, this._data.GetVersion());
    }

    [Test]
    public void Data_Version_NegativeValue() {

    	this._data.SetVersion(1);
    	this._data.SetVersion(-1);
        Assert.AreEqual(1, this._data.GetVersion());
    }

    [Test]
    public void Data_Version_SimpleValue() {

    	this._data.SetVersion(1);
    	this._data.SetVersion(2);
        Assert.AreEqual(2, this._data.GetVersion());
    }


    /**
     *	Flag
     */
    [Test]
    public void Data_Flag_Default() {

        Assert.AreEqual(1, this._data.GetFlag());
    }

    [Test]
    public void Data_Flag_ZeroValue() {

    	this._data.SetFlag(1);
    	this._data.SetFlag(0);
        Assert.AreEqual(0, this._data.GetFlag());
    }

    [Test]
    public void Data_Flag_NegativeValue() {

    	this._data.SetFlag(1);
    	this._data.SetFlag(-1);
        Assert.AreEqual(1, this._data.GetFlag());
    }

    [Test]
    public void Data_Flag_SimpleValue() {

    	this._data.SetFlag(0);
    	this._data.SetFlag(1);
        Assert.AreEqual(1, this._data.GetFlag());
    }

    [Test]
    public void Data_Flag_OORValue() {

    	this._data.SetFlag(1);
    	this._data.SetFlag(2);
        Assert.AreEqual(1, this._data.GetFlag());
    }


    /**
     *	Mtype
     */
    [Test]
    public void Data_Mtype_Default() {

        Assert.AreEqual(1, this._data.GetMtype());
    }

    [Test]
    public void Data_Mtype_ZeroValue() {

    	this._data.SetMtype(1);
    	this._data.SetMtype(0);
        Assert.AreEqual(0, this._data.GetMtype());
    }

    [Test]
    public void Data_Mtype_NegativeValue() {

    	this._data.SetMtype(1);
    	this._data.SetMtype(-1);
        Assert.AreEqual(1, this._data.GetMtype());
    }

    [Test]
    public void Data_Mtype_SimpleValue() {

    	this._data.SetMtype(1);
    	this._data.SetMtype(2);
        Assert.AreEqual(2, this._data.GetMtype());
    }

    [Test]
    public void Data_Mtype_OORValue() {

    	this._data.SetMtype(1);
    	this._data.SetMtype(3);
        Assert.AreEqual(1, this._data.GetMtype());
    }


    /**
     *	SS
     */
    [Test]
    public void Data_SS_Default() {

        Assert.AreEqual(0, this._data.GetSS());
    }

    [Test]
    public void Data_SS_NegativeValue() {

    	this._data.SetSS(0);
    	this._data.SetSS(-1);
        Assert.AreEqual(-1, this._data.GetSS());
    }

    [Test]
    public void Data_SS_SimpleValue() {
    	
    	this._data.SetSS(0);
    	this._data.SetSS(10);
        Assert.AreEqual(10, this._data.GetSS());
    }


    /**
     *	Method
     */
    [Test]
    public void Data_Method_Default() {

        Assert.IsNull(this._data.GetMethod());
    }

    [Test]
    public void Data_Method_EmptyValue() {

    	this._data.SetMethod("FPData_Method_EmptyValue");
    	this._data.SetMethod("");
        Assert.AreEqual("FPData_Method_EmptyValue", this._data.GetMethod());
    }

    [Test]
    public void Data_Method_NullValue() {

    	this._data.SetMethod("FPData_Method_NullValue");
    	this._data.SetMethod(null);
    	Assert.AreEqual("FPData_Method_NullValue", this._data.GetMethod());
    }

    [Test]
    public void Data_Method_SimpleValue() {

    	this._data.SetMethod("FPData_Method");
    	this._data.SetMethod("FPData_Method_SimpleValue");
    	Assert.AreEqual("FPData_Method_SimpleValue", this._data.GetMethod());
        Assert.AreEqual(25, this._data.GetSS());
    }


    /**
     *	Seq
     */
    [Test]
    public void Data_Seq_Default() {

        Assert.AreEqual(0, this._data.GetSeq());
    }

    [Test]
    public void Data_Seq_NegativeValue() {

    	this._data.SetSeq(0);
    	this._data.SetSeq(-1);
        Assert.AreEqual(0, this._data.GetSeq());
    }

    [Test]
    public void Data_Seq_SimpleValue() {

    	this._data.SetSeq(0);
    	this._data.SetSeq(1);
        Assert.AreEqual(1, this._data.GetSeq());
    }


    /**
     *	MsgpackPayload
     */
    [Test]
    public void Data_MsgpackPayload_Default() {

    	Assert.IsNull(this._data.MsgpackPayload());
    }

    [Test]
    public void Data_MsgpackPayload_EmptyValue() {

    	byte[] payload = new byte[10];

    	this._data.SetPayload(payload);
    	this._data.SetPayload(new byte[0]);
    	Assert.AreEqual(payload, this._data.MsgpackPayload());
    }

    [Test]
    public void Data_MsgpackPayload_NullValue() {

        byte[] nullValue = null;
        byte[] payload = new byte[10];

        this._data.SetPayload(payload);
        this._data.SetPayload(nullValue);
        Assert.AreEqual(payload, this._data.MsgpackPayload());
    }

    [Test]
    public void Data_MsgpackPayload_SimpleValue() {

    	byte[] payload = new byte[10];
    	
    	this._data.SetPayload(payload);
    	this._data.SetPayload(new byte[20]);
    	Assert.AreNotEqual(payload, this._data.MsgpackPayload());
    	Assert.AreEqual(20, this._data.GetPsize());
    }


    /**
     *	JsonPayload
     */
    [Test]
    public void Data_JsonPayload_Default() {

    	Assert.IsNull(this._data.JsonPayload());
    }

    [Test]
    public void Data_JsonPayload_EmptyValue() {

    	this._data.SetPayload("{}");
    	this._data.SetPayload("");
    	Assert.AreEqual("{}", this._data.JsonPayload());
    }

    [Test]
    public void Data_JsonPayload_NullValue() {

        string value = null;
        this._data.SetPayload("{}");
        this._data.SetPayload(value);
        Assert.AreEqual("{}", this._data.JsonPayload());
    }

    [Test]
    public void Data_JsonPayload_SimpleValue() {

    	this._data.SetPayload("{}");
    	this._data.SetPayload("{[]}");
    	Assert.AreEqual("{[]}", this._data.JsonPayload());
    	Assert.AreEqual(4, this._data.GetPsize());
    }


    /**
     *	Psize
     */
    [Test]
    public void Data_Psize_Default() {

    	Assert.AreEqual(0, this._data.GetPsize());
    }

    [Test]
    public void Data_Psize_ZeroValue() {

    	this._data.SetPsize(1);
    	this._data.SetPsize(0);
    	Assert.AreEqual(0, this._data.GetPsize());
    }

    [Test]
    public void Data_Psize_NegativeValue() {

    	this._data.SetPsize(1);
    	this._data.SetPsize(-1);
    	Assert.AreEqual(1, this._data.GetPsize());
    }

    [Test]
    public void Data_Psize_SimpleValue() {

    	this._data.SetPsize(1);
    	this._data.SetPsize(10);
    	Assert.AreEqual(10, this._data.GetPsize());
    }


    /**
     *	PkgLen
     */
    [Test]
    public void Data_PkgLen_Default() {

    	Assert.AreEqual(0, this._data.GetPkgLen());
    }

    [Test]
    public void Data_PkgLen_ZeroValue() {

    	this._data.SetPkgLen(1);
    	this._data.SetPkgLen(0);
    	Assert.AreEqual(0, this._data.GetPkgLen());
    }

    [Test]
    public void Data_PkgLen_NegativeValue() {

    	this._data.SetPkgLen(1);
    	this._data.SetPkgLen(-1);
    	Assert.AreEqual(1, this._data.GetPkgLen());
    }

    [Test]
    public void Data_PkgLen_SimpleValue() {

    	this._data.SetPkgLen(1);
    	this._data.SetPkgLen(100);
    	Assert.AreEqual(100, this._data.GetPkgLen());
    }
}
