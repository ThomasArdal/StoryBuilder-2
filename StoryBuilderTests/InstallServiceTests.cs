﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using StoryBuilder.Models;

namespace StoryBuilderTests
{
    [TestClass]
    public class InstallServiceTests
    {   
        private readonly Assembly libAssembly;
        private string[] libResources;

        [TestMethod]
        public void TestResources()
        {
            Assert.AreEqual(27, libResources.Length);
            Assert.AreEqual("StoryBuilder.Assets.Install.Bibliog.txt", libResources[0]);
            Assert.AreEqual(@"StoryBuilder.Assets.Install.samples.A Doll's House.stbx", libResources[19]);
            Assert.AreEqual("StoryBuilder.Assets.Install.samples.The Old Man and the Sea.stbx", libResources[26]);
        }

        [TestMethod]
        public void TestRootDirectory()
        {
            //TODO: Verify GlobalData.RootDirectory contains all resources
            Assert.AreEqual(0, 0);
        }

        public InstallServiceTests()
        {
            libAssembly = Assembly.GetAssembly(typeof(StoryModel));
            libResources = libAssembly.GetManifestResourceNames();
        }

    }
}
