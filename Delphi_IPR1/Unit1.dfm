object Form1: TForm1
  Left = 0
  Top = 0
  Caption = #1057#1087#1080#1089#1086#1082' '#1090#1086#1074#1072#1088#1086#1074
  ClientHeight = 360
  ClientWidth = 634
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 400
    Top = 1
    Width = 57
    Height = 21
    Caption = #1050#1086#1088#1079#1080#1085#1072': '
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Haettenschweiler'
    Font.Style = [fsItalic]
    ParentFont = False
  end
  object Label2: TLabel
    Left = 404
    Top = 281
    Width = 53
    Height = 19
    Caption = #1062#1077#1085#1072': '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Tahoma'
    Font.Style = [fsBold, fsItalic]
    ParentFont = False
  end
  object Label3: TLabel
    Left = 8
    Top = 0
    Width = 64
    Height = 20
    Caption = #1055#1054#1048#1057#1050':'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Trajan Pro 3'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label4: TLabel
    Left = 545
    Top = 272
    Width = 15
    Height = 32
    Caption = '0'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -27
    Font.Name = 'Rosewood Std Regular'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object ProductsListBox: TListBox
    Left = 8
    Top = 27
    Width = 377
    Height = 242
    ItemHeight = 13
    TabOrder = 0
  end
  object BasketListBox: TListBox
    Left = 391
    Top = 24
    Width = 235
    Height = 242
    ItemHeight = 13
    TabOrder = 1
  end
  object Button1: TButton
    Left = 8
    Top = 280
    Width = 169
    Height = 25
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1090#1086#1074#1072#1088
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 2
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 192
    Top = 280
    Width = 193
    Height = 25
    Caption = #1056#1077#1076#1072#1082#1090#1080#1088#1086#1074#1072#1090#1100
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 8
    Top = 312
    Width = 169
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 4
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 192
    Top = 311
    Width = 193
    Height = 25
    Caption = #1042' '#1082#1086#1088#1079#1080#1085#1091
    Font.Charset = EASTEUROPE_CHARSET
    Font.Color = clBlue
    Font.Height = -13
    Font.Name = 'Trajan Pro'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 5
    OnClick = Button4Click
  end
  object Edit1: TEdit
    Left = 78
    Top = 0
    Width = 145
    Height = 21
    TabOrder = 6
    OnChange = Edit1Change
  end
  object Button5: TButton
    Left = 456
    Top = 310
    Width = 170
    Height = 25
    Caption = #1050#1059#1055#1048#1058#1068
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Trajan Pro'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 7
    OnClick = Button5Click
  end
end
