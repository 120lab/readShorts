(function(ionic) {

var SwipeableCardController = ionic.views.View.inherit({
  defaultCardHeight: '300px',

  initialize: function(opts) {
    this.cards = [];
    // Initialize from the passed in options
  },
  pushCard: function(card) {
    var cardMaxHeight = 300;
    var cardsMinMarginFromBottom = 70;
    var cardOffsetFromTop = 160;

     var newSize = this.cards.push(card);
    this.cards[newSize-1].currentIndex = newSize-1;

    var calculatedCardHeight = Math.min(cardMaxHeight,(window.innerHeight - cardsMinMarginFromBottom - cardOffsetFromTop));
    var cardsHeight = calculatedCardHeight  + 'px' ;
    card.el.style.height = cardsHeight;
    if (newSize == 1) {

      card.el.style.display='block';
    } else  

    if (newSize>1 && newSize<4 ) {
      var top = -((newSize-1) * 25);
      var scale = Math.max(0, (1 - ((newSize-1) / 10)));
      card.el.style.transform = card.el.style.webkitTransform = 'translate3d(0, ' + top + 'px, 0) scale('+ scale +')';

      card.el.style.display='block';
    }
    else 
      card.el.style.display='none';
 
    //update z-Index
    for (var i=0;i<newSize;i++) {
      this.cards[i].el.style.zIndex=newSize-i;
    }
  },
  popCard: function() {
    var card = this.cards.shift();
    // Animate the card out
    this.partial(0);
  },
  partial: function(amt) {
   // cards = $element[0].querySelectorAll('td-card');
    for(var i = 1; i < this.cards.length; i++){
      this.bringCardUp(this.cards[i], amt, 25 * i, i);
    }
  },
  bringCardUp: function(card, amt, max, i) {
    //console.log(card, amt, max, i);
    var position, newTop;
    position = card.el.style.transform || card.el.style.webkitTransform;
    newTop = -Math.max(max - 25, Math.min(max, max - (max * Math.abs(amt))));
    newScale = (1 - (Math.max(i - 1, Math.min(i, i - (i * Math.abs(amt)))) / 10));
    card.el.style.transform = card.el.style.webkitTransform = 'translate3d(0, ' + newTop + 'px, 0) scale('+ newScale+')';
    if (i<3) {
      card.el.style.display='block';
    }
    else 
      card.el.style.display='none';
  },

  updateIndex: function() {
    for(var i = 1; i < this.cards.length; i++){
      this.cards[i].currentIndex -= 1;
      this.cards[i].el.style.zIndex = parseInt(this.cards[i].el.style.zIndex) + 1;
    }
  },
  sortCards: function() {
    var existingCards, card;
    existingCards = this.cards;
    var cardsHeight;
    var cardMaxHeight = 300;
    var cardsMinMarginFromBottom = 70;

    for(i = 0; i < existingCards.length; i++) {

      card = existingCards[i];
      card.currentIndex = i;

      if(!card) continue;
      //based on the first card, calculate its height relative to the window. The max size of the card is 300, 
      //but it maybe smaller depending on the space left on screen
      if (i==0) {
        var calculatedCardHeight = Math.min(cardMaxHeight,(window.innerHeight - cardsMinMarginFromBottom - existingCards[i].el.getBoundingClientRect().top));
        cardsHeight = calculatedCardHeight  + 'px' ;
        this.defaultCardHeight = cardsHeight;
      }
      existingCards[i].el.style.height = cardsHeight;
      if(i > 0 && i<3) {

        (function(j) {
          setTimeout(function() {
            var top = -(j * 25);
            var scale = Math.max(0, (1 - (j / 10)));
            var animation = collide.animation({
              duration: 800,
              percent: 0,
              reverse: false
            })

            .easing({
              type: 'spring',
              frequency: 5,
              friction: 250,
              initialForce: false
            })

            .on('step', function(v) {
              existingCards[j].el.style.transform = existingCards[j].el.style.webkitTransform = 'translate3d(0, ' + top*v + 'px, 0) scale('+ scale*v +')';
            })
            .start();
          }, 100 * j);
        })(i);

      }
      card.el.style.zIndex = (existingCards.length - i);
    }
}

});


var SwipeableCardView = ionic.views.View.inherit({
  initialize: function(opts) {
    // Store the card element
    opts = ionic.extend({
	    }, opts);

    ionic.extend(this, opts);
    
    this.el = opts.el;
	this.parentWidth = this.el.parentNode.offsetWidth;

	this.width = this.el.offsetWidth;

	this.startX = this.startY = this.x = this.y = 0;
    this.bindEvents();
  },

  /**
  * marks if the card is draggable or not
  */
  setDraggable: function(i_draggable) {
    this.draggable=i_draggable;
  },

  

  isUnderThreshold: function() {
    return Math.abs(this.thresholdAmount) < this.minThreshold;
  },
  /**
  * Animation to fly the card away
  */
  animateFlyAway: function(e) {

      // is animation triggered by drag or click?
      var draggable = (e !== undefined);

      var self = this;

      //self.updateIndex();

//      self.onTransitionOut(self.thresholdAmount);

      // defaults for animation triggered by click
      var defaults = {
        thresholdAmount: 0,
        rotationAngle: -0.5,
        deltaX: -400,
        deltaY: 400,
        velocityX: 0.1,
        targetX: -1000,
        targetY: -1000,
        duration: 0.4
      }

      this.rotationAngle = this.rotationAngle || defaults.rotationAngle;
      this.thresholdAmount = this.thresholdAmount || defaults.thresholdAmount;

      var deltaX = (draggable) ? e.gesture.deltaX : defaults.deltaX;
      var deltaY = (draggable) ? e.gesture.deltaY : defaults.deltaY;

      var angle = Math.atan(deltaX / deltaY);

      var targetX;
      if(draggable) {
        targetX = (this.x > 0) ? (this.parentWidth ) + (this.width) : - (this.parentWidth + this.width);
      } else {
        targetX = defaults.targetX;
      }

      // Target Y is just the "opposite" side of the triangle of targetX as the adjacent edge (sohcahtoa yo)
      var targetY;
      if(draggable) {
        targetY = targetX / Math.tan(angle);
      } else {
        targetY = defaults.targetY;
      }

      // Fly out
      var rotateTo = this.rotationAngle;

      var velocityX = (draggable) ? e.gesture.velocityX : defaults.velocityX;

      var duration = (draggable) ? (0.3 - Math.min(Math.max(Math.abs(velocityX)/10, 0.05), 0.2)) : defaults.duration;

      ionic.requestAnimationFrame(function() {
        self.el.style.transform = self.el.style.webkitTransform = 'translate3d(' + targetX + 'px, ' + targetY + 'px,0) rotate(' + self.rotationAngle + 'rad)';
        self.el.style.transition = self.el.style.webkitTransition = 'all ' + duration + 's ease-in-out';
      });

      // Trigger destroy after card has swiped out
      setTimeout(function() {
        self.onDestroy && self.onDestroy();
  //      self.onUpdateIndex();
      }, duration * 500);

  },

  /**
  * Fly the card out or animate back into resting position.
  */
  transitionOut: function(e) {
    var self = this;

    if(this.isUnderThreshold()) {
      self.onSnapBack(this.x, this.y, this.rotationAngle);
      return;
    }
    self.animateFlyAway(e);
  },
  bindEvents: function() {
    var self = this;

    ionic.onGesture('dragstart', function(e) {
      // Process start of drag
        ionic.requestAnimationFrame(function() { self._doDragStart(e) });
    }, this.el);

    ionic.onGesture('drag', function(e) {
      // Process drag
        ionic.requestAnimationFrame(function() { self._doDrag(e) });
        // Indicate we want to stop parents from using this
        e.gesture.srcEvent.preventDefault();
    }, this.el);

    ionic.onGesture('dragend', function(e) {
      // Process end of drag
        ionic.requestAnimationFrame(function() { self._doDragEnd(e) });
    }, this.el);
  },
  _doDragStart: function(e) {
      e.preventDefault();
      if(this.currentIndex !== 0) return;
      if(this.draggable=='false') return;
      var width = this.el.offsetWidth;
      var point = window.innerWidth / 2 + this.rotationDirection * (width / 2)
      var distance = Math.abs(point - e.gesture.touches[0].pageX);// - window.innerWidth/2);

      this.touchDistance = distance * 10;
    },

  _doDrag: function(e) {
    e.preventDefault();
    if(this.currentIndex !== 0) return;
    if(this.draggable=='false') return;
    // Calculate how far we've dragged, with a slow-down effect
    var o = e.gesture.deltaX / 1000;

    // Get the angle of rotation based on the
    // drag distance 
    this.rotationAngle = Math.atan(o);

    // Don't rotate if dragging up
    if(e.gesture.deltaY < 0) {
      this.rotationAngle = 0;
    }

    // Update the x&y position of this view
    this.x = this.startX + (e.gesture.deltaX * 0.8);
    this.y = this.startY + (e.gesture.deltaY * 0.8);

    // Apply the CSS transformation to the card,
    // translating it up or down, and rotating
    // it based on the computed angle
    this.el.style.transform = this.el.style.webkitTransform = 'translate3d(' + this.x + 'px, ' + this.y  + 'px, 0) rotate(' + (this.rotationAngle || 0) + 'rad)';

    this.thresholdAmount = (this.x / (this.parentWidth/2));

    var self = this;
    setTimeout(function() {
      self.onPartialSwipe(self.thresholdAmount);
    });
  },
  _doDragEnd: function(e) {
    if(this.currentIndex !== 0) return;
    if(this.draggable=='false') return;
    
    this.transitionOut(e);
  }
});



///////////////////////////////
// Our module, requiring the 'ionic' module
angular.module('ku-cards', ['ionic'])

.directive('swipeCard', ['$timeout', function($timeout) {
  return {
    restrict: 'E',
    template: '<div class="swipe-card" ng-transclude></div>',

    // Requiring the swipeCards directive makes the controller available
    // in the linking function
    require: '^swipeCards',
    replace: true,
    transclude: true,
    scope: {
      drag: '@',
      onSwipe: '&',
      onSnapBack: '&',
      onDestroy: '&'
    },
    compile: function(element, attr) {
      return function($scope, $element, $attr, swipeCards) {
        var el = $element[0];

        // Instantiate our card view
        var swipeableCard = new SwipeableCardView({
          el: el,
          drag: $scope.drag,
          minThreshold: $scope.minThreshold || 0.4,
          onSwipe: function() {
            $timeout(function() {
              $scope.onSwipe();
            });
          },
          onPartialSwipe: function(amt) {
            swipeCards.partial(amt);
            var self = this;
          },
          onUpdateIndex: function() {
            swipeCards.updateIndex();
          },
          onDestroy: function() {
            $timeout(function() {
              swipeCards.updateIndex();
              swipeCards.popCard();
              $scope.onDestroy();
            });
          },
          onSnapBack: function(startX, startY, startRotation) {
            var animation = collide.animation({
              // 'linear|ease|ease-in|ease-out|ease-in-out|cubic-bezer(x1,y1,x2,y2)',
              // or function(t, duration),
              // or a dynamics configuration (see below)
              duration: 500,
              percent: 0,
              reverse: false
            })
            .easing({
              type: 'spring',
              frequency: 15,
              friction: 250,
              initialForce: false
            })
            .on('step', function(v) {
              //Have the element spring over 400px
              el.style.transform = el.style.webkitTransform = 'translate3d(' + (startX - startX*v) + 'px, ' + (startY - startY*v) + 'px, 0) rotate(' + (startRotation - startRotation*v) + 'rad)';
            })
            .start();

            $timeout(function() {
              $scope.onSnapBack();
            });

          }
        });

        // Make the card available to the parent scope, not necessary
        // but makes it easier to interact with (similar to iOS exposing
        // parent controllers and views dynamically to children)
        $scope.$parent.swipeCard = swipeableCard;

        // We can push a new card onto the controller card stack, animating it in
        swipeCards.pushCard(swipeableCard);

        //monitor if card was set to 'draggable' on the directive, and update internal flag accordingly
        $scope.$watch('drag', function(value){
          if(value){
            swipeableCard.setDraggable(value);
          }
        });

      }
    }
  }
}])
.directive('swipeCards', ['$rootScope','$timeout', function($rootScope,$timeout) {
  return {
    restrict: 'E',
    template: '<div class="swipe-cards" ng-transclude></div>',
    replace: true,
    transclude: true,
    scope: {},
    controller: function($scope, $element) {
      // Instantiate the controller
      var swipeController = new SwipeableCardController({

      });

  //  No need to sort cards at init. We sort it as every new card is added
  //      $timeout(function() {
  //        swipeController.sortCards();
  //     });

      // We add a root scope event listener to facilitate interacting with the
      // directive incase of no direct scope access
      $rootScope.$on('swipeCard.pop', function(isAnimated) {
        swipeController.popCard(isAnimated);
      });

      // return the object so it is accessible to child
      // directives that 'require' this directive as a parent.
      return swipeController;
    }
  }
}]);


})(window.ionic);