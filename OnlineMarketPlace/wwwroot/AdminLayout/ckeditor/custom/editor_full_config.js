/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */
CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	
  // تعیین زبان فارسی برای محیط برنامه
  config.language = 'fa';
  // تعیین یک پوسته به دلخواه خود برای ظاهر برنامه
  config.skin= 'moonocolor',
  // تعین زبان نوشتاری از راست به چپ
  config.contentsLangDirection= 'rtl',
  // غیر فعال کردن تغییر سایز در برنامه
  //config.resize_enabled= false,
  // حذف پلاگین تعیین موقعیت متون در استاتوس بار برنامه
  //config.removePlugins = 'elementspath';
  // انتخاب رنگ منو های ابزار در برنامه
  //config.uiColor = '#AADC6E';
  // تعیین میزان عرض ادیتور متن
  //config.width='650px'
  // تعیین میزان ارتفاع ادیتور برنامه
  config.height='300px'
  config.extraPlugins = "syntaxhighlight,codemirror";
  // Toolbar configuration generated automatically by the editor based on config.toolbarGroups.
  // نمایش کلیه امکانات در منو ابزار برنامه
  config.toolbar = [
	  { name: 'document', groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates' ] },
	  { name: 'clipboard', groups: [ 'clipboard', 'undo' ], items: [ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo' ] },
	  { name: 'editing', groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', '-', 'SelectAll', '-', 'Scayt' ] },
	  { name: 'forms', items: [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
	  '/',
	  { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat' ] },
	  { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ], items: [ 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', 'syntaxhighlight', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language' ] },
	  { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
	  { name: 'insert', items: [ 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'syntaxhighlight', 'PageBreak', 'Iframe' ] },
	  '/',
	  { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
	  { name: 'colors', items: [ 'TextColor', 'BGColor' ] },
	  { name: 'tools', items: [ 'Maximize', 'ShowBlocks' ] },
	  { name: 'others', items: [ '-' ] },
	  { name: 'about', items: [ 'About' ] }
  ];
};
/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */