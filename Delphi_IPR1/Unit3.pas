unit Unit3;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Tovar;

type
  TForm3 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Button1: TButton;
    Button2: TButton;
    procedure FormActivate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);


  private
    { Private declarations }
    fSelProd:TProduct;

  public
    { Public declarations }

    procedure SetSelProd(value:TProduct);
    property SelProd:TProduct read  fSelProd write SetSelProd;
  end;



implementation

{$R *.dfm}


procedure TForm3.SetSelProd(value:TProduct);
begin
  fSelProd:= value;
end;


procedure TForm3.FormActivate(Sender: TObject);
begin

    Edit1.Text := SelProd.Title;
    Edit2.Text := IntToStr(SelProd.Price);
    Edit3.Text := IntToStr(SelProd.SumUnits);
end;

procedure TForm3.Button1Click(Sender: TObject);
var
  p:TProduct;
begin
    p:=Self.SelProd;
    p.Title:=Edit1.Text;
    p.Price:=StrToInt(Edit2.Text);
    p.SumUnits:=StrToInt(Edit3.Text);
end;


procedure TForm3.Button2Click(Sender: TObject);
begin
    Self.Close;
end;

end.
