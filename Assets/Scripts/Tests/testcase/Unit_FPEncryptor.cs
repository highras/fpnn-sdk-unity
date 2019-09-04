using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

using com.fpnn;

public class Unit_FPEncryptor {

    private FPEncryptor _cry;

    [SetUp]
    public void SetUp() {

        this._cry = new FPEncryptor(new FPPackage());
    }

    [TearDown]
    public void TearDown() {}

    [Test]
    public void FPEncryptor_NullValue() {

        FPEncryptor cry =  new FPEncryptor(null);
        Assert.IsNotNull(cry);
    }

    [Test]
    public void FPEncryptor_Cryptoed_Default() {

        Assert.IsFalse(this._cry.GetCryptoed());
    }

    [Test]
    public void FPEncryptor_Cryptoed_SimpleValue() {

        this._cry.SetCryptoed(false);
        this._cry.SetCryptoed(true);
        Assert.IsTrue(this._cry.GetCryptoed());
    }

    [Test]
    public void FPEncryptor_Clear() {

        this._cry.SetCryptoed(true);
        this._cry.Clear();
        Assert.IsFalse(this._cry.GetCryptoed());
    }

    [Test]
    public void FPEncryptor_PeekHead_0Bytes_NoCryptoed() {

        this._cry.SetCryptoed(false);
        FPData data = this._cry.PeekHead(new byte[0]);
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_0Bytes_Cryptoed() {

        this._cry.SetCryptoed(true);
        FPData data = this._cry.PeekHead(new byte[0]);
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_10Bytes_NoCryptoed() {

        this._cry.SetCryptoed(false);
        FPData data = this._cry.PeekHead(new byte[10]);
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_10Bytes_Cryptoed() {

        this._cry.SetCryptoed(true);
        FPData data = this._cry.PeekHead(new byte[10]);
        Assert.IsNotNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_16Bytes_NoCryptoed() {

        this._cry.SetCryptoed(false);
        FPData data = this._cry.PeekHead(new byte[16]);
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_16Bytes_Cryptoed() {

        this._cry.SetCryptoed(true);
        FPData data = this._cry.PeekHead(new byte[16]);
        Assert.IsNotNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_EmptyData_NoCryptoed() {

        this._cry.SetCryptoed(false);
        FPData data = this._cry.PeekHead(new FPData());
        Assert.IsNotNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_EmptyData_Cryptoed() {

        this._cry.SetCryptoed(true);
        FPData data = this._cry.PeekHead(new FPData());
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_NullData_NoCryptoed() {

        FPData nullData = null;
        this._cry.SetCryptoed(false);
        FPData data = this._cry.PeekHead(nullData);
        Assert.IsNull(data);
    }

    [Test]
    public void FPEncryptor_PeekHead_NullData_Cryptoed() {

        FPData nullData = null;
        this._cry.SetCryptoed(true);
        FPData data = this._cry.PeekHead(nullData);
        Assert.IsNull(data);
    }
}
