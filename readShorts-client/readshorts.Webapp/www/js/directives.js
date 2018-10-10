
// This file hold all the Custom directives used in Shorts app


/* backgroundImage - directive to add a background image to an element. 
*  				usage: add the 'background-image' attribute to the element
*				example: <div background-image="http://shorts.com/image.png"> 
*/
angular.module('shorts.directives',[]).
directive('backgroundImage', function () {
    return {
    	link: function (scope, element, attrs) {
			scope.$watch(attrs.backgroundImage, function(newVal) {
	    		updateBackgroundImage(newVal);
    		});

    	    function updateBackgroundImage(imageUrl) {
    	    	if (imageUrl == null)
    	    		return;
		        element.css({
		            'background-image': 'url(' + imageUrl + ')',
		            'background-repeat': 'no-repeat',
		            'background-size': 'cover'
		        });
    		}
	    }
	}
})
.directive('dynamicTransparancy', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {          
			/*scope.$watch(attrs.myTransparent, function (value) {     
				element.css('background-color', (value ? 'transparent' : attrs.myBgcolor));            
			}); */
			
			scope.$on('scroll-pos', function(event, args) {
			    var scrollPos = args.yPos;
				element.css('background-color', 'rgba(42,42,42,'+Math.abs(scrollPos/3-40)/40 +')');
				if (scrollPos > 120)
					element.css('visibility', 'hidden');
				else
					element.css('visibility', 'initial');
			});                 
        }
    }
})
/**
*	A directive to make a short Modal height reach the bottom interactive bar
*/
.directive('fullHeightModal', function($window) {

    return {
    	restrict: 'A',
    	link: function (scope, element, attrs) {
			var cardsMinMarginFromBottom = 50;
			var marginFromTop = attrs.top ? attrs.top : 50;
	    	var winHeight = $window.innerHeight;

	    	element.css('top', marginFromTop  + 'px')
	    	element.css('height', winHeight - cardsMinMarginFromBottom -  marginFromTop + 'px')
    	}
    }
})
/**
*	A directive to set the height of the opened/closed card
*/
.directive('fullHeightCard', function($window) {

    return {
    	restrict: 'A',
    	link: function (scope, element, attrs) {
			var cardsMinMarginFromBottom = 70;
			var marginFromTop = attrs.top ? attrs.top : 0;
	    	var winHeight = $window.innerHeight;
	    	var cardOffsetFromTopWhenOpened = -110;
	    	var cardOffsetFromTopWhenClosed = 160;
	    	var cardMaxHeight = 300;

	    	var cardHeight = Math.min(cardMaxHeight,(winHeight - marginFromTop - cardsMinMarginFromBottom - cardOffsetFromTopWhenClosed));
	    	console.log('cardHeight ---->>>>>',cardHeight);

	    	element.css('top', marginFromTop  + 'px')
	    	element.css('height', cardHeight + 'px')

	    	attrs.$observe('cardOpened', function(val){
				if (val=='true') {
			    	//element.css('top', cardOffsetFromTopWhenOpened  + 'px');
/*					element.css('transition', 'height 1.3s, transform 1.7s');
					element.css('-webkit-transition', 'height 0.3s, -webkit-transform 1.7s');
					element.css('transform', 'translate3d(0,'+ cardOffsetFromTopWhenOpened +'px, 0)');
					element.css('-webkit-transform', 'translate3d(0,'+ cardOffsetFromTopWhenOpened +'px, 0)'); */

			    	setTimeout(function() {element.css('height', winHeight - cardsMinMarginFromBottom + 'px')},0);
				}
				else {
			    	//element.css('top', cardOffsetFromTopWhenClosed  + 'px');
/*					element.css('transition', 'height 1.3s, transform 1.7s');
					element.css('-webkit-transition', 'height 0.3s, -webkit-transform 1	.7s');
					element.css('transform', 'translate3d(0,'+ cardOffsetFromTopWhenClosed +'px, 0)');
					element.css('-webkit-transform', 'translate3d(0,'+ cardOffsetFromTopWhenClosed +'px, 0)');*/


			    	setTimeout(function() {element.css('height', cardHeight + 'px')},0);
			    }
			});
    	}
    }
})
/**
* Facebook comments directive
* to use it in the code just add the following:
* <div class="fb-comments" data-href="{{getLocation()}}" data-numposts="10" data-colorscheme="light" data-width="580" id="commentbox"></div>
*/
.directive('fbComments', function() {
   return {
    restrict: 'C',
    link: function(scope, element, attributes) { 
      element[0].dataset.href = document.location.href;
      return typeof FB !== "undefined" && FB !== null ? FB.XFBML.parse(element.parent()[0]) : void 0;
    }
  }
})
/**
* flipDigit - A directive to hadle a single flip digit animation
*			The directive expects a value 'targetdigit' which sets the target counter value
*/
.directive('flipDigit', ['$templateCache','$interval','$interpolate','$animate', function($templateCache, $interval,
	$interpolate, $animate){
	return {
		restrict: 'EA',
		scope: {
		},
		link: function($scope, element, iAttrs, controller) {
			var digit = 0;
			var target = 0;
			$scope.next = 0;
			$scope.current = 0;

			setTimeout(function() {$animate.addClass(compileTemplate(),'change');},0);
			
			function compileTemplate() {
				var template = $templateCache.get('timerTemplate.html');
				var interpolatedTemplate = $interpolate(template)($scope);
				element.html($interpolate(template)($scope));
				var elem = angular.element( document.querySelector( '#'+iAttrs.id+' .inner-box' ) );
				return elem;
			};

			function loadTempalte() {
				var ticker = $interval(function(){
					if ($scope.next == target) {
						$interval.cancel(ticker);
						ticker = undefined;
						return;
					}
					$scope.current = getDigit();
					$scope.next = $scope.current+1;
					if ($scope.next==10)
						$scope.next = 0;
					$animate.addClass(compileTemplate(),'change');
					tickDigit();
				},500);
			};
			function getDigit() {
				return  digit;
			};

			function tickDigit() {
				if (digit == 9) {
					digit = 0;
				}
				else {
					digit += 1;
				}
			}
			iAttrs.$observe('targetdigit', function(val){
				target = val;
				if (target==digit) {
					tickDigit();
					loadTempalte();
				}
				else
					loadTempalte();
			});
		}
	};
}]);