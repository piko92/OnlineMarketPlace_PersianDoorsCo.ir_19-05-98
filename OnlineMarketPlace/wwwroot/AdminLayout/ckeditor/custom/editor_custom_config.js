/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
  config.language = 'fa';
  config.skin= 'moonocolor',
  config.contentsLangDirection= 'rtl',
  config.resize_enabled= false,
  config.removePlugins = 'elementspath';
  //config.uiColor = '#AADC6E';
  //config.width='876px'
  //config.height='300px'
  config.extraPlugins = "syntaxhighlight";
  // Toolbar configuration generated automatically by the editor based on config.toolbarGroups.
  config.toolbar = [
	  { items: ['Templates', 'clipboard', 'Cut', 'Paste',  'Redo', 'Undo','Find', '-','basicstyles', 'cleanup', 'Link', 'Unlink', 'Iframe', 'Anchor','Image', 'Smiley', 'Flash', 'Table', 'SpecialChar', 'Syntaxhighlight', 'HorizontalRule', 'PageBreak', 'ShowBlocks','-', 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'Blockquote' , 'Maximize', 'Preview'] },
	  { items: [ 'Format', 'Font', 'FontSize','-','Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript', '-','NumberedList', 'BulletedList', 'Indent', 'Outdent', '-', 'JustifyBlock', 'JustifyRight', 'JustifyCenter','JustifyLeft', 'BidiRtl', 'BidiLtr', 'TextColor', 'BGColor', 'Source' ] }
  ];

}; 