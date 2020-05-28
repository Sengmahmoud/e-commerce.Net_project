$(function(){
// write all jquery scruipt here inside this function	
 'use strict';
//start navbar edit//////////////////
/*$('.myNave ul li').click(function(){
 ;$(this).attr('class','colored');
});*/


$(window).scroll(function(){
	$('.imagePos').css({top:447+$(window).scrollTop()});
});
if($('.loadIm').text()!="")
{
	 $('.myImage').attr('src',$('.loadIm').text());
	 $('.imgPath').attr('value',$('.loadIm').text());
}

//////////start newadd page/////////////////////
if($('.faildAdvtm').text().trim()=='')
	{
		$('.faildAdvtm').fadeOut();
	}
if ($('.advertised').text()=="passed")
{
	if($('.faildAdvtm').text().trim()!='')
	{
		$('.faildAdvtm').hide();
	}
}
else
{
	$('.faildAdvtm').fadeOut();
}
$('form').click(function(){
$('.successedAdvtm').fadeOut();
$('.faildAdvtm').fadeOut();
});

$('.live').keyup(function(){//using custom attribute
	  $($(this).data('class')).text($(this).val());
  });
   
  $('#selectName').change(function(){
	  changeType();
  });
  $('#selectMaterial').change(function(){
	  changeMaterial();
  });
  if($('.logged').text()=="passed")
  {
	  changeType();
	  changeMaterial();
  }
//////////end newadd page/////////////////////

/////////////start admin login///////////L745 T324
$('.adminForm input').focus(function(){ 
	 $('.errorLoginMessage').fadeOut();
});
$('input').each(
function()
{
  if($(this).attr('required')==="required"&&$(this).attr('type')!="password")
  {
	  $(this).after('<span class="marking">*</span>');
  }
  if($(this).attr('type')==="password")
  {
	  $(this).after('<span class="eye"><i class="fa fa-eye"></i></span>');
  }
});
$('.eye').hover(function(){
$('.password').attr('type','text');});
////////////end admin login//////////////
//start navbar edit//////////////////
 //confirmation message on button
$('.confirmingUser').click(function (){
	
	return confirm("are you sure to delete this member?");
});
$('.confirmingType').click(function (){
	
	return confirm
	("You will delete all products that relate wth this type Are you sure to delete this type?");
});
$('.confir mingComment').click(function (){
	
	return confirm("are you sure to delete this comment?");
});
$('.confirmingProduct').click(function (){
	
	return confirm("are you sure to delete this product?");
});
$('.confirmingOffer').click(function (){
	
	return confirm("are you sure to delete this Offer?");
});
$('.confirmingCategory').click(function (){
	
	return confirm("are you sure to delete this category?");
}); 
$('.confirmingType').click(function (){//not used
	
	return confirm("are you sure to delete this type?");
}); 
$('.confirmingMaterial').click(function (){//not used
	
	return confirm("are you sure to delete this material?");
}); 
$('.confirmingStyle').click(function (){//not used
	
	return confirm("are you sure to delete this style?");
}); 
/////////////////////////////////////////////////////////////////////////////////
/* category view option*/
$('.cat h3').click(function (){
	
	$(this).next('.fullView').fadeToggle(500);
});
/////////////////////////////////////////////////////////////////////////////////
$('.main-view .full-view').click(function(){
	$(this).addClass('active');
	$('.main-view .classic-view').removeClass('active');
	$('.cat .fullView').fadeIn(500);
	
});
/////////////////////////////////////////////////////////////////////////////////
$('.main-view .classic-view').click(function(){
	$(this).addClass('active');
	$('.main-view .full-view').removeClass('active');
	$('.cat .fullView').fadeOut(500);
});
/////////////////////////////////////////////////////////////////////////////////
$('.plus-icon').click(function (){
	$(this).toggleClass('marked').parent().next('.panel-body').fadeToggle(300);
	if($(this).hasClass('marked'))
	{
		$(this).html('<i class="fa fa-minus"></i>');
	}
	else
	{
		$(this).html('<i class="fa fa-plus"></i>');
	}
});
/////////////////////////////////////////////////////////////////////////////////
/*
**this function used to animate control form from above to down 
*/
//for add
var btn='';
var dis='0';
 
 $('.hasBtn .btn-primary').click(function(){
	if($(this).hasClass('style'))
	{
		$('.add .btn').html("<i class='fa fa-plus fa-fw fa-xs'></i>Add style");
		$('.add .control-label').html("Style name");
	    var myInput=document.getElementById('inp');
		myInput.placeholder='Enter the style name';
		btn="style";
		dis="493px";
	}
	else if($(this).hasClass('name'))
	{
		$('.add .btn').html("<i class='fa fa-plus fa-fw fa-xs'></i>Add name");
		$('.add .control-label').html("Product name");
		var myInput=document.getElementById('inp');
		myInput.placeholder='Enter the product name';
		btn="name";
		dis="3px";
	}
	else
	{
		$('.add .btn').html("<i class='fa fa-plus fa-fw fa-xs'></i>Add material");
		$('.add .control-label').html("Material name");
		var myInput=document.getElementById('inp');
		myInput.placeholder='Enter the material name';
		btn="material";
		dis="554px";
	}
	$('.form-horizontal .add').animate({top:dis},1000,function(){
	$('.form-horizontal .add input').animate({left:'10px'},1000);
	$('.form-horizontal .add  .btn').animate({right:'172px'},100);
	var myInput=document.getElementById('inp');
	myInput.focus();
	});
});
 
 
  $('.form-horizontal .addField .fa-close').click(function(){
	 var p=$(this).parent();
	 p.animate({top:'-300px'},2000);
 });
 
 $('.styleD').click(function(){
	  $('#selectStyle :selected').remove();
  });
  
  $('.nameD').click(function(){
	  $('#selectName :selected').remove();
  });
  
  $('.materialD').click(function(){alert("gg");
	  $('#selectMaterial :selected').remove();
  });
///////////////////////////////////////////////////////////////////////////////////
//for add
$('.form-horizontal .add .btn').click(function(){
	var myInput=document.getElementById('inp');
	var selStyle=document.getElementById("selectStyle");
	var selMaterial=document.getElementById("selectMaterial");
	var selName=document.getElementById("selectName");
	var error=false;
	if(btn=="style")
	{
		 for(var i=0;i<selStyle.length;i++)
		 {
			 if(myInput.value.trim()==selStyle[i].textContent.trim())
			 {
				myInput.value="";
			    myInput.placeholder="this style already exist";
				error=true;
			    break;
			 }
		 }
	}
	
	else if(btn=="name")
	{
		for(var i=0;i<selName.length;i++)
		 {
			 if(myInput.value.trim()==selName[i].textContent.trim())
			 {
				myInput.value="";
			    myInput.placeholder="this name already exist";
				error=true;
			    break;
			 }
		 }
	}
	
	else
	{
		for(var i=0;i<selMaterial.length;i++)
		 {
			 if(myInput.value.trim()==selMaterial[i].textContent.trim())
			 {
				myInput.value="";
			    myInput.placeholder="this material already exist";
				error=true;
			    break;
			 }
		 }
	}
	 if(error==false&&myInput.value.trim()=="")
	 {
		 myInput.placeholder="you must enter valid name";
		 error=true;
	 }
	  if(error==false)
	 {
		 if(btn=="style")
		 {
			 SavehangesStyle();
		 }
		 else if(btn=="name")
		 {
		     SavehangesName();	 
		 }
		 else
		 {
		     SavehangesMaterial();	 
		 }
	$(this).parent().animate({top:'-300px'});
	 }
	});
 
////////////////////////////////////////////////////////////////////////////////////
 
});

////////////////////////////////////////////////////////////////////////////////////
function SavehangesStyle()
{
	var option=document.createElement('option');
	option.selected="selected";
	var myInput=document.getElementById('inp');
	option.textContent=myInput.value;
	myInput.value="";
	$('.forStyle .form-control').append(option);
}
function SavehangesMaterial()
{
	var option=document.createElement('option');
	option.selected="selected";
	var myInput=document.getElementById('inp');
	option.textContent=myInput.value;
	myInput.value="";
	$('.forMaterial .form-control').append(option);
}
function SavehangesName()
{
	var option=document.createElement('option');
	option.selected="selected";
	var myInput=document.getElementById('inp');
	option.textContent=myInput.value;
	myInput.value="";
	$('.forName .form-control').append(option);
}
 	 
function loadStyleValues()
{
   $('#deleteSelected').empty();
   var myselect=document.getElementById('deleteSelected');//identify the selected value to be deleted
   var styleSelect=document.getElementById('selectStyle');//get values from select style
   var deletedStyle='';
   for(var i=0;i<styleSelect.length;i++)
   {
	   var option=document.createElement('option');
	   option.textContent=styleSelect[i].textContent;
	   $('#deleteSelected').append(option);
   }	
}
 function loadMaterialValues()
{   
   $('#deleteSelected').empty();
   var myselect=document.getElementById('deleteSelected');//identify the selected value to be deleted
   var materialSelect=document.getElementById('selectMaterial');//get values from select style
   var deletedStyle='';
   for(var i=0;i<materialSelect.length;i++)
   {
	   var option=document.createElement('option');
	   option.textContent=materialSelect[i].textContent;
	   $('#deleteSelected').append(option);
   }	
}

function loadNameValues()
{
   $('#deleteSelected').empty();
   var myselect=document.getElementById('deleteSelected');//identify the selected value to be deleted
   var nameSelect=document.getElementById('selectName');//get values from select style
   var deletedStyle='';
   for(var i=0;i<nameSelect.length;i++)
   {
	   var option=document.createElement('option');
	   option.textContent=nameSelect[i].textContent;
	   $('#deleteSelected').append(option);
   }	
}
function changeType()
 {
	 $('#selectName').find('option:selected').map(function () {
	 $('.livePreview h3').text($(this).text());
  });
 }
 function changeMaterial()
 {
	 $('#selectMaterial').find('option:selected').map(function () {
	 $('.liveMaterial').text($(this).text());
  });
 }
