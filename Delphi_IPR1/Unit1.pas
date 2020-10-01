unit Unit1;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Generics.Collections, Tovar, Unit2, Unit3;

type
  TForm1 = class(TForm)
    ProductsListBox: TListBox;
    BasketListBox: TListBox;
    Edit1: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;

    procedure FormCreate(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure Edit1DblClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);


  private
    { Private declarations }
    fShop:TShop;
    fProducts: TList<TProduct>;
    fBasket: TList<TProduct>;
    fSelProduct: TProduct;
  public
    { Public declarations }
    procedure SetShop(value:TShop);
    procedure ShowProducts;
    procedure ShowBasket;
    function Num(ListBox:TListBox):integer;


    property Shop:TShop read fShop write SetShop;
    property Products:TList<TProduct> read fProducts;
    property Basket:TList<TProduct> read fBasket;


  end;


implementation

{$R *.dfm}



procedure TForm1.SetShop(value:TShop);
begin
  fShop:=value;
end;

                    //////////////////////***********
procedure TForm1.ShowProducts;
var
   p: TProduct;
begin
   ProductsListBox.Clear;
   for p in Shop.Products do
     ProductsListBox.Items.Add(p.ToString);
end;

procedure TForm1.ShowBasket;
var
    p:TProduct;
begin
  BasketListBox.Clear;
  for p in Shop.Basket do
    BasketListBox.Items.Add(p.ToString);
end;

function TForm1.Num(ListBox:TListBox):integer;              //********
begin
    result:=ListBox.ItemIndex;
end;










    //щелчок на кнопке ДОБАВИТЬ
procedure TForm1.Button1Click(Sender: TObject);
var
  Form2:TForm2;

begin
     Form2:=TForm2.Create(Owner);

     Form2.Shop:=Shop;
     //Form2.SetFocus;
     //Form2.Visible:=True;
     Form2.ShowModal;
     //
     //Shop.ShowProductList(ProductsListBox);
     ShowProducts;
end;


   //щелчок на кнопке РЕДАКТИРОВАТЬ
procedure TForm1.Button2Click(Sender: TObject);
var
  num:integer;
  Form3: TForm3;
begin
      num:=Self.Num(ProductsListBox);
      Form3:=TForm3.Create(Owner);
      Form3.SelProd:=Shop.Products[num];
      if Self.Num(ProductsListBox) >= 0 then  Form3.ShowModal;
    //активация формы только при выбранном элементе списка

      ShowProducts;
end;


    //щелчок на кнопке УДАЛИТЬ
procedure TForm1.Button3Click(Sender: TObject);
var
  num:integer;
begin
   num:=Self.Num(ProductsListBox);

    if num >= 0 then
       begin
        if MessageDlg('Товар будет удалён!', mtWarning,[mbOk,mbCancel],0) = mrOk
               then
               begin
               Shop.RemoveProduct(Shop.Products[num]);
               //Shop.ShowProductList(ProductsListBox);
               ShowProducts;
               end;
       end;

 end;


    //щелчок на кнопке ВКОРЗИНУ
procedure TForm1.Button4Click(Sender: TObject);
var
    curr:TProduct;
    num:integer;
begin
    num:=Self.Num(ProductsListBox);
    if num >= 0 then
    begin
    curr:=Shop.Products[num];
    try Shop.AddBasketProduct(curr);
    except on E:TMyException do ShowMessage(E.Message);
    end;

    ShowBasket;
    ShowProducts;
     label4.Caption:=IntToStr(Shop.BuyAll);
    end;
end;




     //щелчок на кнопке КУПИТЬ
procedure TForm1.Button5Click(Sender: TObject);
begin
    Shop.SaveToFile;
    Shop.Basket.Free;
    ShowBasket;
    ShowMessage('Спасибо! Покупка сохранена');
end;




procedure TForm1.Edit1Change(Sender: TObject);
var
    i: integer;
begin
    for i:=0 to Shop.Products.Count-1 do
      begin
        if Pos(Edit1.Text, Shop.Products[i].Title)>0 then
            begin
              ProductsListBox.ItemIndex := i;
              Break;
            end;
      end;

  //if Length(Edit1.Text) <> 0 then
    //for i:=0 to ProductsListBox.Items.Count-1 do
    //if ProductsListBox.Items.Strings[i].Copy(ProductsListBox.Items[i], 1, Pos(';',ProductsListBox.Items[i])-1) = Edit1.Text then
    //ProductsListBox.Selected[i]:= true;
end;

procedure TForm1.Edit1DblClick(Sender: TObject);
begin
    Edit1.Clear;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
   Monitor,Klaviatura,TV,Notebook,Notebook2,Notebook3,Phone1,Phone2,Icebox: TProduct;

begin
 //для быстрой проверки
     Monitor := TProduct.Create('Монитор Viewsonic', 160, 16);
     Klaviatura:= TProduct.Create('Клавиатура SVEN', 5, 222);
     TV:= TProduct.Create('Телевизор LG', 300, 10);
     Notebook:= TProduct.Create('Ноутбук Lenovo G50-30', 500, 9);
     Notebook2:= TProduct.Create('Ноутбук ASUS', 880, 23);
     Notebook3:=TProduct.Create('Ноутбук HP 250 G6', 333,5);
     Phone1:=TProduct.Create('Xiaomi Redmi 4X',160,18);
     Phone2:=TProduct.Create('Samsung Galaxy S9',700,6);
     Icebox:=TProduct.Create('Холодильник Indesit DF 4160',300,21);

     Shop:=TShop.Create;
     Shop.AddProduct(Monitor);
     Shop.AddProduct(Klaviatura);
     Shop.AddProduct(TV);
     Shop.AddProduct(Notebook);
     Shop.AddProduct(Notebook2);
     Shop.AddProduct(Notebook3);
     Shop.AddProduct(Phone1);
     Shop.AddProduct(Phone2);
     Shop.AddProduct(Icebox);

     ShowProducts;

end;






end.
