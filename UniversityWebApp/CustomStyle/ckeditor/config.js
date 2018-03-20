/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
 config.language = 'vi';
	// config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = '/CustomStyle/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/CustomStyle/ckfinder/ckfinder.html?type=Images';
    config.filebrowserUploadUrl = '/CustomStyle/ckfinder/connector?command=QuickUpload&type=Files&currentFolder=/archive/';
    config.filebrowserImageUploadUrl = '/CustomStyle/ckfinder/connector?command=QuickUpload&type=Images&currentFolder=/cars/';
};
