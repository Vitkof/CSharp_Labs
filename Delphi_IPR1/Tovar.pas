unit Tovar;

interface

uses
    Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Generics.Collections;

type
  TMyException = class(Exception)
  end;

  TProduct = class
   private
    fTitle: string;
    fPrice: integer;
    fSumUnits: integer;

    procedure SetTitle(Value:string);
    procedure SetPrice(Value:integer);
    procedure SetSumUnits(Value:integer);

   public
    function ToString: string;
    function Buy(Count: integer): integer;


    constructor Create(NewTitle:string; NewPrice, NewSumUnits: integer);

    property Title:string read fTitle write SetTitle;
    property Price:integer read fPrice write SetPrice;
    property SumUnits:integer read fSumUnits write SetSumUnits;

  end;


  TShop = class
    private
     fProducts: TList<TProduct>;
     fBasket: TList<TProduct>;
    public
     procedure SetProducts(Value:TList<TProduct>);
     procedure SetBasket(Value:TList<TProduct>);




     function GetElemProducts(Ind:integer):TProduct;
     //procedure SetElemProducts(Ind:integer; Value:TProduct);
     function GetElemBasket(Ind:integer):TProduct;
     //procedure SetElemBasket(Ind:integer; Value:TProduct);




     function BuyAll: integer;
     function isInProducts(p:TProduct):Boolean;
     function isInBasket(p:TProduct):Boolean;

     procedure AddProduct(p: TProduct);
     procedure AddBasketProduct(p: TProduct);
     procedure RemoveProduct(p: TProduct);
     procedure SaveToFile;

     property Products:TList<TProduct> read fProducts write SetProducts;
     property Basket:TList<TProduct> read fBasket write SetBasket;



     property  ElemProducts[I:integer]:TProduct read GetElemProducts; //write SetElemProducts;
     property  ElemBasket[I:integer]:TProduct read GetElemBasket; // write SetElemBasket;

     constructor Create;
     destructor Destroy;
  end;





implementation

{ TProduct }

procedure TProduct.SetPrice(Value: integer);
begin
   if Value>0 then fPrice:=Value
   else raise Exception.Create('Недопустимая цена');
end;

procedure TProduct.SetSumUnits(Value: integer);
begin
   if Value>=0 then fSumUnits:=Value
   else raise Exception.Create('Недопустимое кол-во');
end;

procedure TProduct.SetTitle(Value: string);
begin
    if Length(Value)>2 then fTitle:=Value
    else raise Exception.Create('Слишком короткое название');
end;


function TProduct.Buy(Count: integer): integer;
begin
    if Count <= SumUnits then  result:=Price*Count
     else
     begin
       Count:=SumUnits;
       result:=Price*Count;
       Exit;
     end;
end;


constructor TProduct.Create(NewTitle: string; NewPrice, NewSumUnits: integer);
begin
    Title := NewTitle;
    Price := NewPrice;
    SumUnits := NewSumUnits;
end;


function TProduct.ToString: string;
begin
  result:= Title +',   '+ Price.ToString +' $,  '+SumUnits.ToString +' шт.';
end;



{ TShop }



procedure TShop.SaveToFile;
var
  f:TextFile;
  fileName:string;
  i:integer;
begin

  fileName:='Покупка.txt';
  AssignFile(f,fileName);
  Rewrite(f);

  for i:=0 to Basket.Count-1 do Writeln(f, ElemBasket[i].ToString);
  Writeln(f,'------------------------');
  Writeln(f,'ИТОГО:       ', IntToStr(BuyAll));

  Flush(f);
  CloseFile(f);
end;




procedure TShop.SetProducts(Value: TList<TProduct>);
begin
  fProducts:=Value;
end;
procedure TShop.SetBasket(Value: TList<TProduct>);
begin
  fBasket:=Value;
end;




function TShop.GetElemProducts(Ind: integer): TProduct;
begin
  if (Ind>=0) and(Ind<=Products.Count-1) then
  Result:=Products[Ind]
  else Result:=nil;
end;

function TShop.GetElemBasket(Ind: integer): TProduct;
begin
  if (Ind>=0) and (Ind<=Basket.Count-1) then
  Result:=Basket[Ind]
  else Result:=nil;
end;





procedure TShop.AddProduct(p: TProduct);
begin
  if not isInProducts(p) then
     Products.Add(p)
  else raise TMyException.Create('Error! Уже есть в Списке!');
     //else ShowMessage('Уже есть в Списке');
end;

procedure TShop.AddBasketProduct(p: TProduct);
begin
   if not isInBasket(p) then
      begin
      Basket.Add(p);
      Products.Remove(p);
      Exit;

      end

      else raise TMyException.Create( 'Error! Уже есть в Корзине!' );
      //ShowMessage('Уже есть в Корзине');
end;

    //функция проверки наличия
function TShop.isInProducts(p:TProduct): Boolean;
var
  i:integer;
begin
   for i:=0 to Products.Count-1 do
    if ElemProducts[i].Title = p.Title then
    begin
      result:=True;
      Exit;
    end;
    result:=False;
end;
    //функция проверки наличия
function TShop.isInBasket(p:TProduct): Boolean;
var
  i:integer;
begin
   for i:=0 to Basket.Count-1 do
    if ElemBasket[i].Title = p.Title then
    begin
      result:=True;
      Exit;
    end;
    result:=False;
end;


function TShop.BuyAll: integer;
var
  p: TProduct;
begin
  result:=0;
  for p in Basket do
    result:=result+p.Buy(p.fSumUnits);
end;



constructor TShop.Create;
begin
       Products := TList<TProduct>.Create;
       Basket := TList<TProduct>.Create;
end;

destructor TShop.Destroy;
begin
       Products.Destroy;
       Basket.Destroy;
end;



procedure TShop.RemoveProduct(p: TProduct);
begin
     if Products.Contains(p) then
     Products.Remove(p);
end;




end.
