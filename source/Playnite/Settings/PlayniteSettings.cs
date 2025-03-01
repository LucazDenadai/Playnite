﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Configuration;
using Playnite.Common;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using Playnite.Metadata;
using Playnite.SDK;

namespace Playnite
{
    public enum AfterLaunchOptions
    {
        None,
        Minimize,
        Close
    }

    public enum AfterGameCloseOptions
    {
        None,
        Restore
    }

    public enum TrayIconType
    {
        [Description("TrayIcon")]
        Default,
        [Description("TrayIconWhite")]
        Bright,
        [Description("TrayIconBlack")]
        Dark
    }

    public enum DefaultIconSourceOptions
    {
        [Description("LOCGameProviderTitle")]
        Library,
        [Description("LOCPlatformTitle")]
        Platform,
        [Description("Playnite")]
        General,
        [Description("LOCNone")]
        None
    }

    public enum DefaultCoverSourceOptions
    {
        [Description("LOCPlatformTitle")]
        Platform,
        [Description("Playnite")]
        General,
        [Description("LOCNone")]
        None
    }

    public enum DefaultBackgroundSourceOptions
    {
        [Description("LOCGameProviderTitle")]
        Library,
        [Description("LOCPlatformTitle")]
        Platform,
        [Description("LOCGameCoverTitle")]
        Cover,
        [Description("LOCNone")]
        None
    }

    public enum TextRenderingModeOptions
    {
        [Description("LOCSettingsTextRenderingModeOptionAuto")]
        Auto = 0,
        [Description("LOCSettingsTextRenderingModeOptionAliased")]
        Aliased = 1,
        [Description("LOCSettingsTextRenderingModeOptionGrayscale")]
        Grayscale = 2,
        [Description("LOCSettingsTextRenderingModeOptionClearType")]
        ClearType = 3
    }

    public enum TextFormattingModeOptions
    {
        [Description("LOCSettingsTextFormattingModeOptionIdeal")]
        Ideal = 0,
        [Description("LOCSettingsTextFormattingModeOptionDisplay")]
        Display = 1
    }

    public class PlayniteSettings : ObservableObject
    {
        private static SDK.ILogger logger = SDK.LogManager.GetLogger();

        public int Version
        {
            get; set;
        } = 3;

        private DetailsVisibilitySettings detailsVisibility = new DetailsVisibilitySettings();
        public DetailsVisibilitySettings DetailsVisibility
        {
            get
            {
                return detailsVisibility;
            }

            set
            {
                detailsVisibility = value;
                OnPropertyChanged();
            }
        }

        private DefaultIconSourceOptions defaultIconSource = DefaultIconSourceOptions.General;
        public DefaultIconSourceOptions DefaultIconSource
        {
            get
            {
                return defaultIconSource;
            }

            set
            {
                defaultIconSource = value;
                OnPropertyChanged();
            }
        }

        private DefaultCoverSourceOptions defaultCoverSource = DefaultCoverSourceOptions.General;
        public DefaultCoverSourceOptions DefaultCoverSource
        {
            get
            {
                return defaultCoverSource;
            }

            set
            {
                defaultCoverSource = value;
                OnPropertyChanged();
            }
        }

        private DefaultBackgroundSourceOptions defaultBackgroundSource = DefaultBackgroundSourceOptions.None;
        public DefaultBackgroundSourceOptions DefaultBackgroundSource
        {
            get
            {
                return defaultBackgroundSource;
            }

            set
            {
                defaultBackgroundSource = value;
                OnPropertyChanged();
            }
        }

        private bool indentGameDetails = true;
        public bool IndentGameDetails
        {
            get
            {
                return indentGameDetails;
            }

            set
            {
                indentGameDetails = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CalculatedGameDetailsIndentation));
            }
        }

        public double CalculatedGameDetailsIndentation
        {
            get
            {
                return IndentGameDetails ? GameDetailsIndentation : Double.NaN;
            }
        }

        private int gameDetailsIndentation = 400;
        public int GameDetailsIndentation
        {
            get
            {
                return gameDetailsIndentation;
            }

            set
            {
                gameDetailsIndentation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CalculatedGameDetailsIndentation));
            }
        }

        private Dock gridViewDetailsPosition = Dock.Right;
        public Dock GridViewDetailsPosition
        {
            get
            {
                return gridViewDetailsPosition;
            }

            set
            {
                gridViewDetailsPosition = value;
                OnPropertyChanged();
            }
        }

        private Dock sideBarPosition = Dock.Left;
        public Dock SideBarPosition
        {
            get
            {
                return sideBarPosition;
            }

            set
            {
                sideBarPosition = value;
                OnPropertyChanged();
            }
        }

        private Dock filterPanelPosition = Dock.Right;
        public Dock FilterPanelPosition
        {
            get
            {
                return filterPanelPosition;
            }

            set
            {
                filterPanelPosition = value;
                OnPropertyChanged();
            }
        }

        private Dock explorerPanelPosition = Dock.Left;
        public Dock ExplorerPanelPosition
        {
            get
            {
                return explorerPanelPosition;
            }

            set
            {
                explorerPanelPosition = value;
                OnPropertyChanged();
            }
        }

        private Dock detailsListPosition = Dock.Left;
        public Dock DetailsListPosition
        {
            get
            {
                return detailsListPosition;
            }

            set
            {
                detailsListPosition = value;
                OnPropertyChanged();
            }
        }

        private bool explorerPanelVisible = false;
        public bool ExplorerPanelVisible
        {
            get
            {
                return explorerPanelVisible;
            }

            set
            {
                explorerPanelVisible = value;
                OnPropertyChanged();
            }
        }

        private double filterPanelWitdh = 240;
        public double FilterPanelWitdh
        {
            get
            {
                return filterPanelWitdh;
            }

            set
            {
                filterPanelWitdh = value;
                OnPropertyChanged();
            }
        }

        private double explorerPanelWitdh = 280;
        public double ExplorerPanelWitdh
        {
            get
            {
                return explorerPanelWitdh;
            }

            set
            {
                explorerPanelWitdh = value;
                OnPropertyChanged();
            }
        }

        private double grdiDetailsWitdh = 350;
        public double GrdiDetailsWitdh
        {
            get
            {
                return grdiDetailsWitdh;
            }

            set
            {
                grdiDetailsWitdh = value;
                OnPropertyChanged();
            }
        }

        private double detailsListWitdh = 350;
        public double DetailsListWitdh
        {
            get
            {
                return detailsListWitdh;
            }

            set
            {
                detailsListWitdh = value;
                OnPropertyChanged();
            }
        }

        private bool showGridItemBackground = true;
        public bool ShowGridItemBackground
        {
            get
            {
                return showGridItemBackground;
            }

            set
            {
                showGridItemBackground = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public double GridItemHeight
        {
            get
            {
                if (GridItemWidth != 0)
                {
                    return GridItemWidth * ((double)gridItemHeightRatio / GridItemWidthRatio);
                }
                else
                {
                    return 0;
                }
            }
        }

        private double gridItemWidth = ViewSettings.DefaultGridItemWidth;
        public double GridItemWidth
        {
            get
            {
                return gridItemWidth;
            }

            set
            {
                gridItemWidth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GridItemHeight));
            }
        }

        [JsonIgnore]
        public AspectRatio CoverAspectRatio => new AspectRatio(GridItemWidthRatio, GridItemHeightRatio);

        private int gridItemWidthRatio = 27;
        public int GridItemWidthRatio
        {
            get
            {
                return gridItemWidthRatio;
            }

            set
            {
                gridItemWidthRatio = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GridItemHeight));
                OnPropertyChanged(nameof(CoverAspectRatio));
            }
        }

        private int gridItemHeightRatio = 38;
        public int GridItemHeightRatio
        {
            get
            {
                return gridItemHeightRatio;
            }

            set
            {
                gridItemHeightRatio = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GridItemHeight));
                OnPropertyChanged(nameof(CoverAspectRatio));
            }
        }

        private Stretch coverArtStretch = Stretch.UniformToFill;
        public Stretch CoverArtStretch
        {
            get
            {
                return coverArtStretch;
            }

            set
            {
                coverArtStretch = value;
                OnPropertyChanged();
            }
        }

        private int gridItemSpacing = 8;
        public int GridItemSpacing
        {
            get
            {
                return gridItemSpacing;
            }

            set
            {
                gridItemSpacing = value;
                OnPropertyChanged();
                ItemSpacingMargin = GetItemSpacingMargin();
                OnPropertyChanged(nameof(ItemSpacingMargin));
            }
        }

        private int gridItemMargin = 2;
        public int GridItemMargin
        {
            get
            {
                return gridItemMargin;
            }

            set
            {
                gridItemMargin = value;
                OnPropertyChanged();
            }
        }

        private int fullscreenItemSpacing = 20;
        public int FullscreenItemSpacing
        {
            get
            {
                return fullscreenItemSpacing;
            }

            set
            {
                fullscreenItemSpacing = value;
                OnPropertyChanged();
                FullscreenItemSpacingMargin = GetFullscreenItemSpacingMargin();
                OnPropertyChanged(nameof(FullscreenItemSpacingMargin));
            }
        }

        [JsonIgnore]
        public Thickness ItemSpacingMargin
        {
            get; private set;
        }

        [JsonIgnore]
        public Thickness FullscreenItemSpacingMargin
        {
            get; private set;
        }

        private bool firstTimeWizardComplete;
        public bool FirstTimeWizardComplete
        {
            get
            {
                return firstTimeWizardComplete;
            }

            set
            {
                firstTimeWizardComplete = value;
                OnPropertyChanged();
            }
        }

        private bool disableHwAcceleration = false;
        [RequiresRestart]
        public bool DisableHwAcceleration
        {
            get
            {
                return disableHwAcceleration;
            }

            set
            {
                disableHwAcceleration = value;
                OnPropertyChanged();
            }
        }

        private bool disableDpiAwareness = false;
        [RequiresRestart]
        public bool DisableDpiAwareness
        {
            get
            {
                return disableDpiAwareness;
            }

            set
            {
                disableDpiAwareness = value;
                OnPropertyChanged();
            }
        }

        private bool asyncImageLoading = false;
        [RequiresRestart]
        public bool AsyncImageLoading
        {
            get
            {
                return asyncImageLoading;
            }

            set
            {
                asyncImageLoading = value;
                OnPropertyChanged();
            }
        }

        private bool showNameEmptyCover = true;
        public bool ShowNameEmptyCover
        {
            get
            {
                return showNameEmptyCover;
            }

            set
            {
                showNameEmptyCover = value;
                OnPropertyChanged();
            }
        }


        private bool showNamesUnderCovers = false;
        public bool ShowNamesUnderCovers
        {
            get
            {
                return showNamesUnderCovers;
            }

            set
            {
                showNamesUnderCovers = value;
                OnPropertyChanged();
            }
        }

        private bool showBackgroundImageOnWindow = true;
        public bool ShowBackgroundImageOnWindow
        {
            get
            {
                return showBackgroundImageOnWindow;
            }

            set
            {
                showBackgroundImageOnWindow = value;
                OnPropertyChanged();
            }
        }

        private bool highQualityBackgroundBlur = true;
        public bool HighQualityBackgroundBlur
        {
            get
            {
                return highQualityBackgroundBlur;
            }

            set
            {
                highQualityBackgroundBlur = value;
                OnPropertyChanged();
            }
        }

        private bool blurWindowBackgroundImage = true;
        public bool BlurWindowBackgroundImage
        {
            get
            {
                return blurWindowBackgroundImage;
            }

            set
            {
                blurWindowBackgroundImage = value;
                OnPropertyChanged();
            }
        }

        private double backgroundImageBlurAmount = 60;
        public double BackgroundImageBlurAmount
        {
            get
            {
                return backgroundImageBlurAmount;
            }

            set
            {
                backgroundImageBlurAmount = value;
                OnPropertyChanged();
            }
        }

        private bool darkenWindowBackgroundImage = true;
        public bool DarkenWindowBackgroundImage
        {
            get
            {
                return darkenWindowBackgroundImage;
            }

            set
            {
                darkenWindowBackgroundImage = value;
                OnPropertyChanged();
            }
        }

        private float backgroundImageDarkAmount = 0.7f;
        public float BackgroundImageDarkAmount
        {
            get
            {
                return backgroundImageDarkAmount;
            }

            set
            {
                backgroundImageDarkAmount = value;
                OnPropertyChanged();
            }
        }

        private bool showBackImageOnGridView = false;
        public bool ShowBackImageOnGridView
        {
            get
            {
                return showBackImageOnGridView;
            }

            set
            {
                showBackImageOnGridView = value;
                OnPropertyChanged();
            }
        }

        private bool downloadMetadataOnImport = true;
        public bool DownloadMetadataOnImport
        {
            get
            {
                return downloadMetadataOnImport;
            }

            set
            {
                downloadMetadataOnImport = value;
                OnPropertyChanged();
            }
        }

        private bool migrationV2PcPlatformAdded = false;
        public bool MigrationV2PcPlatformAdded
        {
            get
            {
                return migrationV2PcPlatformAdded;
            }

            set
            {
                migrationV2PcPlatformAdded = value;
                OnPropertyChanged();
            }
        }

        private bool showIconsOnList = true;
        public bool ShowIconsOnList
        {
            get
            {
                return showIconsOnList;
            }

            set
            {
                showIconsOnList = value;
                OnPropertyChanged();
            }
        }

        private bool showGroupCount = false;
        public bool ShowGroupCount
        {
            get
            {
                return showGroupCount;
            }

            set
            {
                showGroupCount = value;
                OnPropertyChanged();
            }
        }

        private bool startInFullscreen = false;
        public bool StartInFullscreen
        {
            get
            {
                return startInFullscreen;
            }

            set
            {
                startInFullscreen = value;
                OnPropertyChanged();
            }
        }

        private string databasePath;
        [RequiresRestart]
        public string DatabasePath
        {
            get
            {
                return databasePath;
            }

            set
            {
                databasePath = value;
                OnPropertyChanged();
            }
        }

        private FilterSettings filterSettings = new FilterSettings();
        public FilterSettings FilterSettings
        {
            get
            {
                return filterSettings;
            }

            set
            {
                filterSettings = value;
            }
        }

        private ViewSettings desktopViewSettings = new ViewSettings();
        public ViewSettings ViewSettings
        {
            get
            {
                return desktopViewSettings;
            }

            set
            {
                desktopViewSettings = value;
            }
        }

        private bool gridViewSideBarVisible = false;
        public bool GridViewSideBarVisible
        {
            get
            {
                return gridViewSideBarVisible;
            }

            set
            {
                gridViewSideBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool filterPanelVisible = false;
        public bool FilterPanelVisible
        {
            get
            {
                return filterPanelVisible;
            }

            set
            {
                filterPanelVisible = value;
                OnPropertyChanged();
            }
        }
                
        private bool notificationPanelVisible = false;
        [JsonIgnore]
        public bool NotificationPanelVisible
        {
            get
            {
                return notificationPanelVisible;
            }

            set
            {
                notificationPanelVisible = value;
                OnPropertyChanged();
            }
        }

        private bool sidebarVisible = false;
        [JsonIgnore]
        public bool SidebarVisible
        {
            get
            {
                return sidebarVisible;
            }

            set
            {
                sidebarVisible = value;
                OnPropertyChanged();
            }
        }

        private Dock sidebarPosition = Dock.Left;
        [JsonIgnore]
        public Dock SidebarPosition
        {
            get
            {
                return sidebarPosition;
            }

            set
            {
                sidebarPosition = value;
                OnPropertyChanged();
            }
        }

        private bool minimizeToTray = false;
        public bool MinimizeToTray
        {
            get
            {
                return minimizeToTray;
            }

            set
            {
                minimizeToTray = value;
                OnPropertyChanged();
            }
        }

        private bool closeToTray = true;
        public bool CloseToTray
        {
            get
            {
                return closeToTray;
            }

            set
            {
                closeToTray = value;
                OnPropertyChanged();
            }
        }

        private bool enableTray = true;
        [RequiresRestart]
        public bool EnableTray
        {
            get
            {
                return enableTray;
            }

            set
            {
                enableTray = value;
                OnPropertyChanged();
            }
        }

        private string language = "english";
        [RequiresRestart]
        public string Language
        {
            get
            {
                return language;
            }

            set
            {
                language = value;
                OnPropertyChanged();
            }
        }

        private bool updateLibStartup = true;
        public bool UpdateLibStartup
        {
            get
            {
                return updateLibStartup;
            }

            set
            {
                updateLibStartup = value;
                OnPropertyChanged();
            }
        }

        private AfterLaunchOptions afterLaunch = AfterLaunchOptions.Minimize;
        public AfterLaunchOptions AfterLaunch
        {
            get
            {
                return afterLaunch;
            }

            set
            {
                afterLaunch = value;
                OnPropertyChanged();
            }
        }

        private AfterGameCloseOptions afterGameClose = AfterGameCloseOptions.Restore;
        public AfterGameCloseOptions AfterGameClose
        {
            get
            {
                return afterGameClose;
            }

            set
            {
                afterGameClose = value;
                OnPropertyChanged();
            }
        }

        private string theme = "Default";
        [RequiresRestart]
        public string Theme
        {
            get
            {
                return theme;
            }

            set
            {
                theme = value;
                OnPropertyChanged();
            }
        }

        private TrayIconType trayIcon = TrayIconType.Default;
        [RequiresRestart]
        public TrayIconType TrayIcon
        {
            get
            {
                return trayIcon;
            }

            set
            {
                trayIcon = value;
                OnPropertyChanged();
            }
        }

        public string InstallInstanceId
        {
            get; set;
        }

        private List<string> disabledPlugins = new List<string>();
        [RequiresRestart]
        public List<string> DisabledPlugins
        {
            get
            {
                return disabledPlugins;
            }

            set
            {
                disabledPlugins = value;
                OnPropertyChanged();
            }
        }

        private bool showSteamFriendsButton = true;
        public bool ShowSteamFriendsButton
        {
            get
            {
                return showSteamFriendsButton;
            }

            set
            {
                showSteamFriendsButton = value;
                OnPropertyChanged();
            }
        }

        private bool startMinimized = false;
        public bool StartMinimized
        {
            get
            {
                return startMinimized;
            }

            set
            {
                startMinimized = value;
                OnPropertyChanged();
            }
        }

        private bool startOnBoot = false;
        public bool StartOnBoot
        {
            get
            {
                return startOnBoot;
            }

            set
            {
                startOnBoot = value;
                OnPropertyChanged();
            }
        }

        private bool forcePlayTimeSync = false;
        public bool ForcePlayTimeSync
        {
            get
            {
                return forcePlayTimeSync;
            }

            set
            {
                forcePlayTimeSync = value;
                OnPropertyChanged();
            }
        }

        private bool enableControolerInDesktop = false;
        [RequiresRestart]
        public bool EnableControllerInDesktop
        {
            get
            {
                return enableControolerInDesktop;
            }

            set
            {
                enableControolerInDesktop = value;
                OnPropertyChanged();
            }
        }

        private bool guideButtonOpensFullscreen = false;
        public bool GuideButtonOpensFullscreen
        {
            get
            {
                return guideButtonOpensFullscreen;
            }

            set
            {
                guideButtonOpensFullscreen = value;
                OnPropertyChanged();
            }
        }

        private bool showPanelSeparators = true;
        public bool ShowPanelSeparators
        {
            get
            {
                return showPanelSeparators;
            }

            set
            {
                showPanelSeparators = value;
                OnPropertyChanged();
            }
        }

        private double gameDetailsCoverHeight = 170;
        public double GameDetailsCoverHeight
        {
            get
            {
                return gameDetailsCoverHeight;
            }

            set
            {
                gameDetailsCoverHeight = value;
                OnPropertyChanged();
            }
        }

        private string fontFamilyName = "Trebuchet MS";
        [RequiresRestart]
        public string FontFamilyName
        {
            get
            {
                return fontFamilyName;
            }

            set
            {
                fontFamilyName = value;
                OnPropertyChanged();
            }
        }

        private double fontSize = 14;
        [RequiresRestart]
        public double FontSize
        {
            get
            {
                return fontSize;
            }

            set
            {
                fontSize = value;
                OnPropertyChanged();
            }
        }

        private double fontSizeSmall = 12;
        [RequiresRestart]
        public double FontSizeSmall
        {
            get
            {
                return fontSizeSmall;
            }

            set
            {
                fontSizeSmall = value;
                OnPropertyChanged();
            }
        }

        private double fontSizeLarge = 15;
        [RequiresRestart]
        public double FontSizeLarge
        {
            get
            {
                return fontSizeLarge;
            }

            set
            {
                fontSizeLarge = value;
                OnPropertyChanged();
            }
        }

        private double fontSizeLarger = 20;
        [RequiresRestart]
        public double FontSizeLarger
        {
            get
            {
                return fontSizeLarger;
            }

            set
            {
                fontSizeLarger = value;
                OnPropertyChanged();
            }
        }

        private double fontSizeLargest = 29;
        [RequiresRestart]
        public double FontSizeLargest
        {
            get
            {
                return fontSizeLargest;
            }

            set
            {
                fontSizeLargest = value;
                OnPropertyChanged();
            }
        }

        private double detailsViewListIconSize = 26;
        public double DetailsViewListIconSize
        {
            get
            {
                return detailsViewListIconSize;
            }

            set
            {
                detailsViewListIconSize = value;
                OnPropertyChanged();
            }
        }

        private TextFormattingModeOptions textFormattingMode = TextFormattingModeOptions.Ideal;
        [RequiresRestart]
        public TextFormattingModeOptions TextFormattingMode
        {
            get
            {
                return textFormattingMode;
            }

            set
            {
                textFormattingMode = value;
                OnPropertyChanged();
            }
        } 

        private TextRenderingModeOptions textRenderingMode = TextRenderingModeOptions.Auto;
        [RequiresRestart]
        public TextRenderingModeOptions TextRenderingMode
        {
            get
            {
                return textRenderingMode;
            }

            set
            {
                textRenderingMode = value;
                OnPropertyChanged();
            }
        }

        private MetadataDownloaderSettings metadataSettings;
        public MetadataDownloaderSettings MetadataSettings
        {
            get
            {
                return metadataSettings;
            }

            set
            {
                metadataSettings = value;
                OnPropertyChanged();
            }
        }

        private ScriptLanguage actionsScriptLanguage = ScriptLanguage.Batch;
        public ScriptLanguage ActionsScriptLanguage
        {
            get => actionsScriptLanguage;
            set
            {
                actionsScriptLanguage = value;
                OnPropertyChanged();
            }
        }

        private string preScript;
        public string PreScript
        {
            get => preScript;
            set
            {
                preScript = value;
                OnPropertyChanged();
            }
        }

        private string postScript;
        public string PostScript
        {
            get => postScript;
            set
            {
                postScript = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public static bool IsPortable
        {
            get
            {
                return !File.Exists(PlaynitePaths.UninstallerPath);
            }
        }

        [JsonIgnore]
        public WindowPositions WindowPositions
        {
            get; private set;
        } = new WindowPositions();

        [JsonIgnore]
        public FullscreenSettings Fullscreen
        {
            get; private set;
        } = new FullscreenSettings();

        public PlayniteSettings()
        {
            InstallInstanceId = Guid.NewGuid().ToString();
            ItemSpacingMargin = GetItemSpacingMargin();
            FullscreenItemSpacingMargin = GetFullscreenItemSpacingMargin();
        }

        private static T LoadSettingFile<T>(string path) where T : class
        {
            try
            {
                if (File.Exists(path))
                {
                    return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                }
            }
            catch (Exception e) when (!PlayniteEnvironment.ThrowAllErrors)
            {
                logger.Error(e, $"Failed to load {path} setting file.");
            }

            return null;
        }

        private static void SaveSettingFile(object settings, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        public static PlayniteSettings LoadSettings()
        {
            var settings = LoadSettingFile<PlayniteSettings>(PlaynitePaths.ConfigFilePath);
            if (settings == null)
            {
                logger.Info("No existing settings found, creating default ones.");
                settings = new PlayniteSettings();
            }
            else
            {
                if (settings.Version == 1)
                {
                    settings.BackgroundImageBlurAmount = 17;
                    settings.Version = 2;
                }

                if (settings.Version == 2)
                {
                    settings.BackgroundImageBlurAmount = 60;
                    settings.Version = 3;
                }
            }

            settings.WindowPositions = LoadSettingFile<WindowPositions>(PlaynitePaths.WindowPositionsPath);
            if (settings.WindowPositions == null)
            {
                logger.Info("No existing WindowPositions settings found, creating default ones.");
                settings.WindowPositions = new WindowPositions();
            }

            settings.Fullscreen = LoadSettingFile<FullscreenSettings>(PlaynitePaths.FullscreenConfigFilePath);
            if (settings.Fullscreen == null)
            {
                logger.Info("No existing fullscreen settings found, creating default ones.");
                settings.Fullscreen = new FullscreenSettings();
            }

            if (settings.MetadataSettings == null)
            {
                settings.MetadataSettings = MetadataDownloaderSettings.GetDefaultSettings();
            }

            return settings;
        }

        public void SaveSettings()
        {
            FileSystem.CreateDirectory(PlaynitePaths.ConfigRootPath);
            SaveSettingFile(this, PlaynitePaths.ConfigFilePath);
            SaveSettingFile(WindowPositions, PlaynitePaths.WindowPositionsPath);
            SaveSettingFile(Fullscreen, PlaynitePaths.FullscreenConfigFilePath);
        }

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            logger.Error(errorContext.Error, $"Failed to deserialize {errorContext.Path}.");
            errorContext.Handled = true;
        }

        public static void ConfigureLogger()
        {
            var config = new LoggingConfiguration();
#if DEBUG
            var consoleTarget = new ColoredConsoleTarget()
            {
                Layout = @"${level:uppercase=true}|${logger}:${message}${exception}"
            };

            config.AddTarget("console", consoleTarget);

            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);
#endif
            var fileTarget = new FileTarget()
            {
                FileName = Path.Combine(PlaynitePaths.ConfigRootPath, "playnite.log"),
                Layout = "${longdate}|${level:uppercase=true}:${message}${exception:format=toString}",
                KeepFileOpen = false,
                ArchiveFileName = Path.Combine(PlaynitePaths.ConfigRootPath, "playnite.{#####}.log"),
                ArchiveAboveSize = 4096000,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                MaxArchiveFiles = 2
            };

            config.AddTarget("file", fileTarget);

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            NLog.LogManager.Configuration = config;
            SDK.LogManager.Init(new NLogLogProvider());
            logger = SDK.LogManager.GetLogger();
        }

        public static string GetAppConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool GetAppConfigBoolValue(string key)
        {
            if (bool.TryParse(ConfigurationManager.AppSettings[key], out var result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        public static void MigrateSettingsConfig()
        {
        }

        public static void SetBootupStateRegistration(bool runOnBootup)
        {
            var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var shortcutPath = Path.Combine(startupPath, "Playnite.lnk");
            if (runOnBootup)
            {
                var args = new CmdLineOptions()
                {
                    HideSplashScreen = true
                }.ToString();
                                
                if (File.Exists(shortcutPath))
                {
                    var existLnk = Programs.GetLnkShortcutData(shortcutPath);
                    if (existLnk.Path == PlaynitePaths.DesktopExecutablePath &&
                        existLnk.Arguments == args)
                    {
                        return;
                    }
                }

                FileSystem.DeleteFile(shortcutPath);
                Programs.CreateShortcut(PlaynitePaths.DesktopExecutablePath, args, "", shortcutPath);
            }
            else
            {
                FileSystem.DeleteFile(shortcutPath);
            }
        }

        private Thickness GetItemSpacingMargin()
        {
            return new Thickness(GridItemSpacing / 2, GridItemSpacing / 2, GridItemSpacing / 2, GridItemSpacing / 2);;
        }

        private Thickness GetFullscreenItemSpacingMargin()
        {
            int marginX = FullscreenItemSpacing / 2;
            int marginY = ((int)CoverAspectRatio.GetWidth(FullscreenItemSpacing) / 2);
            return new Thickness(marginY, marginX, 0, 0);
        }

        #region Serialization Conditions

        public bool ShouldSerializeDisabledPlugins()
        {
            return DisabledPlugins.HasItems();
        }       

        #endregion Serialization Conditions
    }
}
