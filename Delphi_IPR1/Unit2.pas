unit Unit2;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Tovar;

type
  TForm2 = class(TForm)
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Button1: TButton;
    Button8: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);



  private
    { Private declarations }
    fShop:TShop;
  public
    { Public declarations }
    procedure SetShop(value:TShop);
    property Shop:TShop read fShop write SetShop;

  end;



implementation

{$R *.dfm}

procedure TForm2.SetShop(value: TShop);
begin
  fShop:=value;
end;
procedure TForm2.Button1Click(Sender: TObject);
begin
        Self.Close;
      // Application.Terminate();

end;
//procedure TForm2.




procedure TForm2.Button8Click(Sender: TObject);
var
  p:TProduct;
begin
     p:=  TProduct.Create(Edit1.Text, StrToInt(Edit2.Text), StrToInt(Edit3.Text));

     if assigned(p) then
      begin
          if not Shop.isInProducts(p) then
          begin
           Edit1.Text := '';
           Edit2.Text := '';
           Edit3.Text := '';
            ShowMessage('Добавлено:    '+p.ToString);
          end;

         try Shop.AddProduct(p);
         except on E:TMyException do ShowMessage(E.Message);
         end;
      end;

end;

end.
