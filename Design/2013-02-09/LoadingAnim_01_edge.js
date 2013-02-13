/**
 * Adobe Edge: symbol definitions
 */
(function($, Edge, compId){
//images folder
var im='images/';

var fonts = {};


var resources = [
];
var symbols = {
"stage": {
   version: "1.0.0",
   minimumCompatibleVersion: "0.1.7",
   build: "1.0.1.204",
   baseState: "Base State",
   initialState: "Base State",
   gpuAccelerate: false,
   resizeInstances: false,
   content: {
         dom: [
         {
            id:'n_fungi',
            type:'image',
            rect:['147px','67px','256px','256px','auto','auto'],
            fill:["rgba(0,0,0,0)",im+"n_fungi.svg",'0px','0px'],
            transform:[[],['0deg']]
         }],
         symbolInstances: [

         ]
      },
   states: {
      "Base State": {
         "${_Stage}": [
            ["color", "background-color", 'rgba(255,255,255,1)'],
            ["style", "overflow", 'hidden'],
            ["style", "height", '400px'],
            ["style", "width", '550px']
         ],
         "${_n_fungi}": [
            ["style", "top", '67px'],
            ["transform", "scaleY", '0'],
            ["transform", "rotateZ", '360deg'],
            ["style", "height", '256px'],
            ["transform", "scaleX", '0'],
            ["style", "left", '147px'],
            ["style", "width", '256px']
         ]
      }
   },
   timelines: {
      "Default Timeline": {
         fromState: "Base State",
         toState: "",
         duration: 2000,
         autoPlay: true,
         timeline: [
            { id: "eid4", tween: [ "transform", "${_n_fungi}", "scaleY", '1', { fromValue: '0'}], position: 0, duration: 1000 },
            { id: "eid9", tween: [ "transform", "${_n_fungi}", "scaleY", '0', { fromValue: '1'}], position: 1000, duration: 1000 },
            { id: "eid2", tween: [ "transform", "${_n_fungi}", "scaleX", '1', { fromValue: '0'}], position: 0, duration: 1000 },
            { id: "eid8", tween: [ "transform", "${_n_fungi}", "scaleX", '0', { fromValue: '1'}], position: 1000, duration: 1000 },
            { id: "eid6", tween: [ "transform", "${_n_fungi}", "rotateZ", '0deg', { fromValue: '360deg'}], position: 0, duration: 1000 },
            { id: "eid7", tween: [ "transform", "${_n_fungi}", "rotateZ", '-360deg', { fromValue: '0deg'}], position: 1000, duration: 1000 }         ]
      }
   }
}
};


Edge.registerCompositionDefn(compId, symbols, fonts, resources);

/**
 * Adobe Edge DOM Ready Event Handler
 */
$(window).ready(function() {
     Edge.launchComposition(compId);
});
})(jQuery, AdobeEdge, "EDGE-27021601");
