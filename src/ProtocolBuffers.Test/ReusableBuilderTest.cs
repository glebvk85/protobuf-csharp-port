﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Google.ProtocolBuffers.TestProtos;

namespace Google.ProtocolBuffers
{
    [TestFixture]
    public class ReusableBuilderTest
    {
        [Test]
        public void TestUnmodifiedDefaultInstance()
        {
            //Simply calling ToBuilder().Build() no longer creates a copy of the message
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void BuildMultipleWithoutChange()
        {
            //Calling Build() or BuildPartial() does not require a copy of the message
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            builder.SetDefaultBool(true);

            TestAllTypes first = builder.BuildPartial();
            //Still the same instance?
            Assert.IsTrue(ReferenceEquals(first, builder.Build()));
            //Still the same instance?
            Assert.IsTrue(ReferenceEquals(first, builder.BuildPartial().ToBuilder().Build()));
        }

        [Test]
        public void MergeFromDefaultInstance()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.MergeFrom(TestAllTypes.DefaultInstance);
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void BuildNewBuilderIsDefaultInstance()
        {
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, new TestAllTypes.Builder().Build()));
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, TestAllTypes.CreateBuilder().Build()));
            //last test, if you clear a builder it reverts to default instance
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance,
                TestAllTypes.CreateBuilder().SetOptionalBool(true).Build().ToBuilder().Clear().Build()));
        }

        [Test]
        public void CloneOnChangePrimitive()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.SetDefaultBool(true);
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnAddRepeatedBool()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.AddRepeatedBool(true);
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnChangeMessage()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.SetOptionalForeignMessage(new ForeignMessage.Builder());
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnClearMessage()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.ClearOptionalForeignMessage();
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnAddRepeatedForeignMessage()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.AddRepeatedForeignMessage(ForeignMessage.DefaultInstance);
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnChangeEnumValue()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.SetOptionalForeignEnum(ForeignEnum.FOREIGN_BAR);
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

        [Test]
        public void CloneOnAddRepeatedForeignEnum()
        {
            TestAllTypes.Builder builder = TestAllTypes.DefaultInstance.ToBuilder();
            Assert.IsTrue(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
            builder.AddRepeatedForeignEnum(ForeignEnum.FOREIGN_BAR);
            Assert.IsFalse(ReferenceEquals(TestAllTypes.DefaultInstance, builder.Build()));
        }

    }
}