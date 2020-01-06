using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MBlog.Components
{
    public partial class MB_Entry : ContentView
    {

        public static readonly BindableProperty MB_EntryLabelProperty =
                          BindableProperty.Create(nameof(MB_EntryLabel),
                                                  typeof(string),
                                                  typeof(MB_Entry),
                                                  string.Empty);

        public string MB_EntryLabel
        {
            get { return (string)GetValue(MB_EntryLabelProperty); }
            set { SetValue(MB_EntryLabelProperty, value); }
        }

        public static readonly BindableProperty MB_EntryTextProperty =
                          BindableProperty.Create(nameof(MB_EntryText),
                                                  typeof(string),
                                                  typeof(MB_Entry),
                                                  string.Empty,
                                                  BindingMode.TwoWay);

        public string MB_EntryText
        {
            get { return (string)GetValue(MB_EntryTextProperty); }
            set { SetValue(MB_EntryTextProperty, value); }
        }

        public static readonly BindableProperty MB_EntryMaxLengthProperty =
                          BindableProperty.Create(nameof(MB_EntryMaxLength),
                                                  typeof(int),
                                                  typeof(MB_Entry),
                                                  int.MaxValue,
                                                  BindingMode.TwoWay);

        public int MB_EntryMaxLength
        {
            get { return (int)GetValue(MB_EntryMaxLengthProperty); }
            set { SetValue(MB_EntryMaxLengthProperty, value); }
        }

        public static readonly BindableProperty MB_EntryIsPasswordProperty =
                          BindableProperty.Create(nameof(MB_EntryIsPassword),
                                                  typeof(bool),
                                                  typeof(MB_Entry),
                                                  false);

        public bool MB_EntryIsPassword
        {
            get { return (bool)GetValue(MB_EntryIsPasswordProperty); }
            set { SetValue(MB_EntryIsPasswordProperty, value); }
        }

        public static readonly BindableProperty MB_EntryTabIndexProperty =
                          BindableProperty.Create(nameof(MB_EntryTabIndex),
                                                  typeof(int),
                                                  typeof(MB_Entry),
                                                  default(int));

        public int MB_EntryTabIndex
        {
            get { return (int)GetValue(MB_EntryTabIndexProperty); }
            set { SetValue(MB_EntryTabIndexProperty, value); }
        }

        public static readonly BindableProperty MB_KeyboardProperty =
                          BindableProperty.Create(nameof(MB_Keyboard),
                                                  typeof(Xamarin.Forms.Keyboard),
                                                  typeof(MB_Entry),
                                                  Xamarin.Forms.Keyboard.Default);

        public Xamarin.Forms.Keyboard MB_Keyboard
        {
            get { return (Xamarin.Forms.Keyboard)GetValue(MB_KeyboardProperty); }
            set { SetValue(MB_KeyboardProperty, value); }
        }

        public static readonly BindableProperty MB_ReturnTypeProperty =
  BindableProperty.Create(nameof(MB_ReturnType),
                          typeof(Xamarin.Forms.ReturnType),
                          typeof(MB_Entry),
                          Xamarin.Forms.ReturnType.Default);

        public Xamarin.Forms.ReturnType MB_ReturnType
        {
            get { return (Xamarin.Forms.ReturnType)GetValue(MB_ReturnTypeProperty); }
            set { SetValue(MB_ReturnTypeProperty, value); }
        }

        public MB_Entry()
        {
            InitializeComponent();
        }
    }
}
