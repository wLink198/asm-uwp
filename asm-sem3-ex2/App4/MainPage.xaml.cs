﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private static StorageFile file;
        private async void ChooseSong(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".mp3");
            
            file = await openPicker.PickSingleFileAsync();
            
            _mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(file.Path));
            _mediaPlayerElement.MediaPlayer.Play();
        }

        private async void ChooseVideo(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".mp4");

            file = await openPicker.PickSingleFileAsync();

            _mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(file.Path));
            _mediaPlayerElement.MediaPlayer.AudioCategory = MediaPlayerAudioCategory.Media;
            _mediaPlayerElement.MediaPlayer.Play();
        }
    }
}
