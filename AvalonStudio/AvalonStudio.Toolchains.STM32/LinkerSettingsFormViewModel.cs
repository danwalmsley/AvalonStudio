﻿namespace AvalonStudio.Toolchains.STM32
{
    using AvalonStudio.MVVM;
    using Extensibility.Utils;
    using Projects;
    using System;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Windows.Input;
    using Toolchains;

    public class LinkSettingsFormViewModel : ViewModel<IProject>
    {
        LinkSettings settings = new LinkSettings();

        public LinkSettingsFormViewModel(IProject project) : base (project)
        {
            try
            {
                if (Model.ToolchainSettings.STM32ToolchainSettings.LinkSettings is ExpandoObject)
                {
                    settings = (Model.ToolchainSettings.STM32ToolchainSettings.LinkSettings as ExpandoObject).GetConcreteType<LinkSettings>();
                }
                else
                {
                    settings = Model.ToolchainSettings.STM32ToolchainSettings.LinkSettings;
                }
            }
            catch (Exception e)
            {
                Model.ToolchainSettings.STM32ToolchainSettings = new STM32ToolchainSettings();
                settings = Model.ToolchainSettings.STM32ToolchainSettings.LinkSettings;
            }

            if(settings == null)
            {
                settings = new LinkSettings();
            }

                        
            useMemoryLayout = settings.UseMemoryLayout;
            discardUnusedSections = settings.DiscardUnusedSections;
            notUseStandardStartup = settings.NotUseStandardStartupFiles;
            linkedLibraries = new ObservableCollection<string>(settings.LinkedLibraries);
            inRom1Start = string.Format("0x{0:X8}", settings.InRom1Start);
            inRom1Size = string.Format("0x{0:X8}", settings.InRom1Size);
            inRom2Start = string.Format("0x{0:X8}", settings.InRom2Start);
            inRom2Size = string.Format("0x{0:X8}", settings.InRom2Size);
            inRam1Start = string.Format("0x{0:X8}", settings.InRam1Start);
            inRam1Size = string.Format("0x{0:X8}", settings.InRam1Size);
            inRam2Start = string.Format("0x{0:X8}", settings.InRam2Start);
            inRam2Size = string.Format("0x{0:X8}", settings.InRam2Size);
            scatterFile = settings.ScatterFile;
            miscOptions = settings.MiscLinkerArguments;
            librarySelectedIndex = (int)settings.Library;

            //AddLinkedLibraryCommand = new RoutingCommand(AddLinkedLibrary);
            //RemoveLinkedLibraryCommand = new RoutingCommand(RemoveLinkedLibrary);
            //BrowseScatterFileCommand = new RoutingCommand(BrowseScatterFile);
            //EditScatterFileCommand = new RoutingCommand(EditScatterFile);

            UpdateLinkerString();
        }

        public void UpdateLinkerString()
        {
            Save();

            //if (project.SelectedConfiguration.ToolChain is StandardToolChain)
            //{
            //    var cTC = project.SelectedConfiguration.ToolChain as StandardToolChain;

            //    LinkerArguments = cTC.GetLinkerArguments(project);
            //}
        }

        private void EditScatterFile(object obj)
        {
            //throw new NotImplementedException();
        }

        private void BrowseScatterFile(object obj)
        {
            // throw new NotImplementedException();
        }

        public void Save()
        {
            settings.UseMemoryLayout = useMemoryLayout;
            settings.DiscardUnusedSections = discardUnusedSections;
            settings.NotUseStandardStartupFiles = notUseStandardStartup;
            settings.LinkedLibraries = linkedLibraries.ToList();
            settings.InRom1Start = Convert.ToUInt32(inRom1Start, 16);
            settings.InRom1Size = Convert.ToUInt32(inRom1Size, 16);
            settings.InRom2Start = Convert.ToUInt32(inRom2Start, 16);
            settings.InRom2Size = Convert.ToUInt32(inRom2Size, 16);
            settings.InRam1Start = Convert.ToUInt32(inRam1Start, 16);
            settings.InRam1Size = Convert.ToUInt32(inRam1Size, 16);
            settings.InRam2Start = Convert.ToUInt32(inRam2Start, 16);
            settings.InRam2Size = Convert.ToUInt32(inRam2Size, 16);
            settings.ScatterFile = scatterFile;
            settings.MiscLinkerArguments = miscOptions;
            settings.Library = (LibraryType)librarySelectedIndex;

            Model.ToolchainSettings.STM32ToolchainSettings.LinkSettings = settings;
            Model.Save();
        }

        private void AddLinkedLibrary(object param)
        {
            //var ofd = new OpenFileDialog();

            //ofd.InitialDirectory = project.CurrentDirectory;

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    string newInclude = project.CurrentDirectory.MakeRelativePath(ofd.FileName);

            //    if (newInclude == string.Empty)
            //    {
            //        newInclude = "\\";
            //    }

            //    LinkedLibraries.Add(newInclude);

            //    UpdateLinkerString();
            //}
        }

        private void RemoveLinkedLibrary(object param)
        {
            LinkedLibraries.Remove(SelectedLinkedLibrary);
            UpdateLinkerString();
        }

        private IProject project;

        public ICommand AddLinkedLibraryCommand { get; private set; }
        public ICommand RemoveLinkedLibraryCommand { get; private set; }
        public ICommand BrowseScatterFileCommand { get; private set; }
        public ICommand EditScatterFileCommand { get; private set; }

        public int MemoryAreasVisible
        {
            get
            {
                if (useMemoryLayout)
                {
                    return 195;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ScatterFileVisible
        {
            get
            {
                if (useMemoryLayout)
                {
                    return 0;
                }
                else
                {
                    return 85;
                }
            }
        }

        private void OnPropertyChanged()
        {

        }

        private void OnPropertyChanged<T> (Expression<Func<T>> changedProperty)
        {

        }

        private bool useMemoryLayout = true;
        public bool UseMemoryLayout
        {
            get { return useMemoryLayout; }
            set { useMemoryLayout = value; OnPropertyChanged(); OnPropertyChanged(() => MemoryAreasVisible); OnPropertyChanged(() => ScatterFileVisible); UpdateLinkerString(); }
        }

        private bool discardUnusedSections;
        public bool DiscardUnusedSections
        {
            get { return discardUnusedSections; }
            set { discardUnusedSections = value; OnPropertyChanged(); UpdateLinkerString(); }
        }


        private bool notUseStandardStartup;
        public bool NotUseStandardStartup
        {
            get { return notUseStandardStartup; }
            set { notUseStandardStartup = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private int librarySelectedIndex;
        public int LibrarySelectedIndex
        {
            get { return librarySelectedIndex; }
            set { librarySelectedIndex = value; OnPropertyChanged(); UpdateLinkerString(); }
        }


        public string[] LibraryOptions
        {
            get
            {               
                return Enum.GetNames(typeof(LibraryType));
            }
        }

        private string selectedLinkedLibrary;
        public string SelectedLinkedLibrary
        {
            get { return selectedLinkedLibrary; }
            set { selectedLinkedLibrary = value; OnPropertyChanged(); UpdateLinkerString(); }
        }


        private ObservableCollection<string> linkedLibraries;
        public ObservableCollection<string> LinkedLibraries
        {
            get { return linkedLibraries; }
            set { linkedLibraries = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRom1Start;
        public string InRom1Start
        {
            get { return inRom1Start; }
            set { inRom1Start = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRom1Size;
        public string InRom1Size
        {
            get { return inRom1Size; }
            set { inRom1Size = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRom2Start;
        public string InRom2Start
        {
            get { return inRom2Start; }
            set { inRom2Start = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRom2Size;
        public string InRom2Size
        {
            get { return inRom2Size; }
            set { inRom2Size = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRam1Start;
        public string InRam1Start
        {
            get { return inRam1Start; }
            set { inRam1Start = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRam1Size;
        public string InRam1Size
        {
            get { return inRam1Size; }
            set { inRam1Size = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRam2Start;
        public string InRam2Start
        {
            get { return inRam2Start; }
            set { inRam2Start = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string inRam2Size;
        public string InRam2Size
        {
            get { return inRam2Size; }
            set { inRam2Size = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string scatterFile;
        public string ScatterFile
        {
            get { return scatterFile; }
            set { scatterFile = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string miscOptions;
        public string MiscOptions
        {
            get { return miscOptions; }
            set { miscOptions = value; OnPropertyChanged(); UpdateLinkerString(); }
        }

        private string linkerArguments;
        public string LinkerArguments
        {
            get { return linkerArguments; }
            set { linkerArguments = value; OnPropertyChanged(); }
        }


        //new private StandardToolChain model;
        //public StandardToolChain Model
        //{
        //    get { return model; }
        //    set { model = value; OnPropertyChanged(); }
        //}

    }
}
