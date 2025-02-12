function redondeo(num,precision)
{
  num = num.toString().replace(/\ |\,/g,'');
  if(isNaN(num)) 
  num = "0";
  cents = Math.floor((num*100+0.5)%100);
  num = Math.floor((num*100+0.5)/100).toString();
  if(cents < 10) 
	cents = "0" + cents;
  for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
	num = num.substring(0,num.length-(4*i+3))+num.substring(num.length-(4*i+3));
	if (precision > 0)
	{
	  return (' ' + num + ',' + cents);
	}
	else if (precision == 0)
	{
	  return (' ' + num);
	}
}

//El 70% de 10 es 7.
function calculoporcentaje(ciento1, cantidad1)
{	
	ciento1=ciento1.toString().replace(',','.');	
	cantidad1=cantidad1.toString().replace(',','.');	
	return ciento1*cantidad1/100;	 
}
function calculoporcentaje2(ciento2, cantidad2)
{

	ciento2=ciento2.toString().replace(',','.');

	cantidad2=cantidad2.toString().replace(',','.');
	
	return 100/ciento2*cantidad2;

}
function calculoporcentaje3(total3,cantidad3)
{

	total3=total3.toString().replace(',','.');
	
	cantidad3=cantidad3.toString().replace(',','.');
	
	return cantidad3/total3*100;
	
}

	

