/* --------------------------------------------- */
/* Author: http://codecanyon.net/user/CodingJack */
/* --------------------------------------------- */

/*global Jacked:true */

;(function($) {
	
	var defaults = {
		
		shineContrast: 1.5, 
		shineBrightness: 30,
		
		zoomBorder: "1px solid rgba(0, 0, 0, 0.7)",
		zoomBorderIE: "1px solid #000",
		
		rubixBorder: "3px solid rgba(0, 0, 0, 0.75)",
		rubixBorderIE: "3px solid #000",
		
		cols: 4,
		rows: 3,
		color: "#FFF",
		reversed: false
		
	},
	
	methods = {
		
		shine: function($this) {
			
			var data = $this.data(),
			ar = writeCanvas($this, {width: data.width, height: data.height}), 
			
			imageData = ar[2],
			context = ar[1],
			canvas = ar[0],
			parent,
			
			settings = data.settings,
			contrast = settings.shineContrast,
			brightness = settings.shineBrightness,
			imgData = imageData.data, 
			p1 = imgData.length - 4, 
			p2, 
			p3, 
			rc, 
			gc, 
			bc;
			
			while(p1 > -1) {
				
				p2 = p1 + 1;
				p3 = p2 + 1;
	
				rc = (imgData[p1] * contrast) + brightness;
				gc = (imgData[p2] * contrast) + brightness;
				bc = (imgData[p3] * contrast) + brightness;
				
				imgData[p1] = rc > 255 ? 255 : rc < 0 ? 0 : rc;
				imgData[p2] = gc > 255 ? 255 : gc < 0 ? 0 : gc;
				imgData[p3] = bc > 255 ? 255 : bc < 0 ? 0 : bc;
				
				p1 -= 4;
				
			}
			
			context.putImageData(imageData, 0, 0);
			context.restore();
			
			canvas.css("opacity", 0).insertAfter($this);
			
			parent = $this.parent();
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", canvas).on("mouseover.cj", overShine);
			
		},
		
		blinds: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			settings = data.settings,
			cols = settings.cols,
			rows = settings.rows,
			color = settings.color,
			
			i = cols * rows,
			newW = Math.ceil(w / cols),
			newH = Math.ceil(h / rows),
			holder,
			square,
			bright,
			row,
			col,
			ww,
			hh,
			
			ar,
			parent,
			arr = [],
			imageData,
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden",
				position: "absolute"
				
			}),
			
			css1 = {
				
				width: newW,
				height: newH,
				display: "block",
				overflow: "hidden",
				position: "absolute"
				
			},
			
			css2 = {
				
				opacity: 0,
				width: newW,
				height: newH,
				display: "block",
				position: "absolute",
				backgroundColor: color
				
			};
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				css1.top = hh;
				css1.left = ww;
				
				holder = $("<span />").css(css1).appendTo(container);
				bright = $("<span />").css(css2).appendTo(holder);
				
				ar = writeCanvas($this, {width: w, height: h});
				imageData = ar[2];
				square = ar[0].css({marginLeft: -ww, marginTop: -hh}).appendTo(holder);
				
				blackWhite(imageData.data);
				ar[1].putImageData(imageData, 0, 0);
				arr[i] = [holder, square, bright];
				
			}
			
			parent = $this.parent();
			container.insertAfter($this);
			
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", arr).on("mouseenter.cj", overOutBlinds).on("mouseleave.cj", overOutBlinds);
			
		},
		
		scramble: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			ar = writeCanvas($this, {width: w, height: h}), 
			imageData = ar[2],
			context = ar[1],
			cnv = ar[0][0],
			parent,
			
			settings = data.settings,
			reversed = settings.reversed,
			cols = settings.cols,
			rows = settings.rows,
			i = cols * rows,
			j = i,
			
			newW = (w / cols) | 0,
			newH = (h / rows) | 0,
			datas = {},
			holder,
			square,
			over,
			out,
			row,
			col,
			ww,
			hh,
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden",
				position: "absolute"
				
			}),
			
			css1 = {
				
				width: newW,
				height: newH,
				display: "block",
				overflow: "hidden",
				position: "absolute"
				
			};
			
			context.putImageData(imageData, 0, 0);
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				datas.origLeft = ww;
				datas.origTop = hh;
				
				holder = ar[i] = $("<span />").data(datas).appendTo(container);
				
				square = $("<canvas />").appendTo(holder);
				square[0].getContext("2d").drawImage(cnv, ww, hh, newW, newH, 0, 0, newW, newH);
				
			}
			
			if(reversed) {
				
				ar = shuffle(ar, j);
				over = overScramble;
				out = outScramble;
				
			}
			else {
			
				over = outScramble;
				out = overScramble;
				
			}
			
			while(j--) {
			
				col = j % cols;
				row = (j / cols) | 0;
				
				css1.left = newW * col;
				css1.top = newH * row;
				
				ar[j].css(css1);
				
			}
			
			parent = $this.parent();
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			
			parent.css({position: "relative", height: h, width: w}).data({
				
				fx: ar,
				cols: cols,
				rows: rows,
				newW: newW,
				newH: newH
				
			}).on("mouseenter.cj", over).on("mouseleave.cj", out);
			
			container.insertAfter($this);
			$this.remove();
			
		},
		
		rubix: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			ar = writeCanvas($this, {width: w, height: h}), 
			imageData = ar[2],
			context = ar[1],
			cnv = ar[0][0],
			parent,
			
			settings = data.settings,
			cols = settings.cols,
			rows = settings.rows,
			color = settings.color,
			
			i = cols * rows,
			newW = Math.ceil(w / cols),
			newH = Math.ceil(h / rows),
			border = !isIE ? settings.rubixBorder : settings.rubixBorderIE,
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden",
				position: "absolute"
				
			}),
			
			bright,
			holder,
			square,
			row,
			col,
			ww,
			hh,
			
			css1 = {
			
				width: newW,
				height: newH,
				display: "block",
				overflow: "hidden",
				position: "absolute"
				
			},
			
			css2 = {
			
				opacity: 0,
				width: newW,
				height: newH,
				border: border,
				display: "block",
				position: "absolute",
				backgroundColor: color,
				boxSizing: "border-box"
				
			};
			
			blackWhite(imageData.data);
			context.putImageData(imageData, 0, 0);
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				css1.left = ww;
				css1.top = hh;
				
				holder = $("<span />").css(css1).appendTo(container);
				bright = $("<span />").css(css2).appendTo(holder);
				square = $("<canvas />").appendTo(holder);
				
				ar[i] = [holder, bright];
				square[0].getContext("2d").drawImage(cnv, ww, hh, newW, newH, 0, 0, newW, newH);
				
			}
			
			parent = $this.parent();
			container.insertAfter($this);
			
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", ar).on("mouseenter.cj", overOutRubix).on("mouseleave.cj", overOutRubix);
			
		},
		
		rotate: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			obj = {width: w, height: h},
			ar = writeCanvas($this, obj), 
			canvas = ar[0][0],
			imageData = ar[2],
			context = ar[1],
			parent,
			
			settings = data.settings,
			cols = settings.cols,
			rows = settings.rows,
			
			i = cols * rows,
			newW = Math.ceil(w / cols),
			newH = Math.ceil(h / rows),
			
			holder2,
			square2,
			holder,
			square,
			cnv,
			row,
			col,
			ww,
			hh,
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden"
				
			}),
			
			css1 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			css2 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			style = container[0].style;
			style[transformStyle] = "preserve-3d";
			
			css2[transform] = "rotateZ(-180deg)";
			context.putImageData(imageData, 0, 0);
			context.restore();
			
			ar = writeCanvas($this, obj);
			imageData = ar[2];
			context = ar[1];
			cnv = ar[0][0];
			
			blackWhite(imageData.data);
			context.putImageData(imageData, 0, 0);
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				css1.left = css2.left = ww;
				css1.top = css2.top = hh;
				
				holder = $("<span />").css(css2).appendTo(container);
				holder2 = $("<span />").css(css1).appendTo(container);
				
				square = $("<canvas />").appendTo(holder);
				square2 = $("<canvas />").appendTo(holder2);
				
				holder[0].cjFx = holder2[0];
				ar[i] = [holder[0], square2[0]];
				
				square[0].getContext("2d").drawImage(canvas, ww, hh, newW, newH, 0, 0, newW, newH);
				square2[0].getContext("2d").drawImage(cnv, ww, hh, newW, newH, 0, 0, newW, newH);
				
			}
			
			parent = $this.parent();
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", ar).on("mouseenter.cj", overOutRotate).on("mouseleave.cj", overOutRotate);
			
			container.insertAfter($this);
			$this.remove();
			
		},
		
		flip: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			obj = {width: w, height: h},
			ar = writeCanvas($this, obj), 
			imageData = ar[2],
			context = ar[1],
			parent,
			
			settings = data.settings,
			cols = settings.cols,
			rows = settings.rows,
			
			i = cols * rows,
			newW = Math.ceil(w / cols),
			newH = Math.ceil(h / rows),
			
			holder2,
			square2,
			holder,
			square,
			row,
			col,
			ww,
			hh,
			
			arr = [],
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden"
				
			}),
			
			css1 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			css2 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			style = container[0].style;
			style[perspective] = "100px";
			style[transformStyle] = "preserve-3d";
			
			css2[transform] = "rotateX(-180deg)";
			context.putImageData(imageData, 0, 0);
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				css1.left = css2.left = ww;
				css1.top = css2.top = hh;
				
				holder = $("<span />").css(css2).appendTo(container);
				holder2 = $("<span />").css(css1).appendTo(container);
				
				ar = writeCanvas($this, {width: w, height: h});
				imageData = ar[2];
				square = ar[0].css({marginLeft: -ww, marginTop: -hh}).appendTo(holder);
				
				ar = writeCanvas($this, {width: w, height: h});
				imageData = ar[2];
				square2 = ar[0].css({marginLeft: -ww, marginTop: -hh}).appendTo(holder2);
				
				blackWhite(imageData.data);
				ar[1].putImageData(imageData, 0, 0);
				
				holder[0].cjFx = holder2[0];
				arr[i] = [holder[0], square2[0]];
				
			}
			
			parent = $this.parent();
			container.insertAfter($this);
			
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", arr).on("mouseenter.cj", overOutFlip).on("mouseleave.cj", overOutFlip);
			
			$this.remove();
			
		},
		
		scale: function($this) {
			
			var data = $this.data(),
			h = data.height,
			w = data.width,
			
			obj = {width: w, height: h},
			ar = writeCanvas($this, obj), 
			canvas = ar[0][0],
			imageData = ar[2],
			context = ar[1],
			parent,
			
			settings = data.settings,
			cols = settings.cols,
			rows = settings.rows,
			
			i = cols * rows,
			newW = Math.ceil(w / cols),
			newH = Math.ceil(h / rows),
			
			holder2,
			square2,
			holder,
			square,
			cnv,
			row,
			col,
			ww,
			hh,
			
			container = $("<div />").css({
				
				top: 0, 
				left: 0, 
				width: w, 
				height: h, 
				overflow: "hidden"
				
			}),
			
			css1 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			css2 = {
			
				position: "absolute",
				overflow: "hidden",
				display: "block",
				height: newH,
				width: newW
				
			},
			
			style = container[0].style;
			style[perspective] = "75px";
			style[transformStyle] = "preserve-3d";
			
			css2[transform] = "scale(0, 0)";
			context.putImageData(imageData, 0, 0);
			
			ar = writeCanvas($this, obj);
			imageData = ar[2];
			context = ar[1];
			cnv = ar[0][0];
			
			blackWhite(imageData.data);
			context.putImageData(imageData, 0, 0);
			context.restore();
			
			while(i--) {
				
				col = i % cols;
				row = (i / cols) | 0;
				
				ww = newW * col;
				hh = newH * row;
				
				css1.left = css2.left = ww;
				css1.top = css2.top = hh;
				
				if(transform) {
					
					holder = $("<span />").css(css2).appendTo(container);
					holder2 = $("<span />").css(css1).appendTo(container);
					
				}
				else {
					
					holder2 = $("<span />").css(css1).appendTo(container);
					holder = $("<span />").css(css2).appendTo(container);
					
				}
				
				square = $("<canvas />").appendTo(holder);
				square2 = $("<canvas />").appendTo(holder2);
				
				holder[0].cjFx = holder2[0];
				ar[i] = holder[0];
				
				square[0].getContext("2d").drawImage(canvas, ww, hh, newW, newH, 0, 0, newW, newH);
				square2[0].getContext("2d").drawImage(cnv, ww, hh, newW, newH, 0, 0, newW, newH);
				
			}
			
			parent = $this.parent();
			container.insertAfter($this);
			
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative").data("fx", ar).on("mouseenter.cj", overOutScale).on("mouseleave.cj", overOutScale);
			
			$this.remove();
			
		},
		
		zoom: function($this) {
			
			var data = $this.data(),
			thumb = data.thumb,
			
			tWidth = parseInt(thumb.attr("width"), 10) || thumb.width(),
			tHeight = parseInt(thumb.attr("height"), 10) || thumb.height(),
			parent = thumb.parent(),
			hHeight = tHeight >> 1,
			qHeight = hHeight >> 1,
			hWidth = tWidth >> 1,
			qWidth = hWidth >> 1,
			
			container = $("<div />").css({
				
				top: 0,
				left: 0,
				opacity: 0,
				width: hWidth,
				height: hHeight,
				overflow: "hidden",
				position: "absolute",
				border: !isIE ? data.settings.zoomBorder : data.settings.zoomBorderIE
				
			}).insertAfter(thumb).append($this);
			
			$("<div />").insertAfter(thumb).data({
				
				ww: qWidth,
				hh: qHeight,
				width: tWidth,
				image: $this[0],
				height: tHeight,
				zoom: container[0],
				bufR: tWidth - qWidth - 2,
				bufB: tHeight - qHeight - 2,
				snapR: tWidth - hWidth - 2,
				snapB: tHeight - hHeight - 2
			
			}).append(thumb, container).on("mouseenter.cj", enterZoom).on("mouseleave.cj", leaveZoom);
			
			if(parent.css("display") === "inline") parent.css("display", "inline-block");
			parent.css("position", "relative");
			
		}
		
	},
	
	hasJacked = typeof Jacked !== "undefined", isIE, transform,
	html5 = "getContext" in document.createElement("canvas"),
	css = {position: "absolute", left: 0, top: 0},
	transformStyle, perspective, tStyle;
	
	if(hasJacked) {
		
		isIE = Jacked.getIE();
		transform = Jacked.getTransform();
		
	}
	
	// ****************************************************
	// END VARS *******************************************
	// ****************************************************

	
	function loaded() {
		
		var $this = $(this), fx = $this.data("fx");
		
		if(methods.hasOwnProperty(fx)) methods[fx]($this);
		
	}
	
	function effect(type, sets) {
		
		var $this = $(this);
		
		if(type !== "zoom") {
			
			var width = parseInt($this.attr("width"), 10) || $this.width(),
			height = parseInt($this.attr("height"), 10) || $this.height(),
			
			clone = $("<img />").attr({
				
				width: width,
				height: height
				
			}).data({
				
				fx: type,
				width: width,
				height: height,
				settings: sets
				
			}).insertAfter($this);
			
			if(html5) clone.one("load.cj", loaded);
			clone.attr("src", $this.attr("data-src"));
			$this.remove();
			
		}
		else {
			
			var src = $this.attr("data-zoom"), image;
			if(!src) return;
			
			image = $("<img />").data({
				
				fx: type,
				thumb: $this,
				settings: sets
				
			}).one("load.cj", loaded);
			
			image.attr("src", src);
			
		}
		
	}
	
	function writeCanvas($this, obj) {

		var canvas = $("<canvas />").attr(obj).css(css), 
		
		width = obj.width,
		height = obj.height,
		
		context = canvas[0].getContext("2d");
		context.drawImage($this[0], 0, 0, width, height);

		return [canvas, context, context.getImageData(0, 0, width, height)];
		
	}
	
	function blackWhite(imgData) {
		
		var p1 = imgData.length - 4, 
		p2, 
		p3, 
		gs;
		
		while(p1 > -1) {
			
			p2 = p1 + 1;
			p3 = p2 + 1;
			gs = imgData[p1] * 0.3 + imgData[p2] * 0.59 + imgData[p3] * 0.11;
			
			imgData[p1] = gs;
			imgData[p2] = gs;
			imgData[p3] = gs;
			
			p1 -= 4;
			
		}
		
	}
	
	function shuffle(ar, i) {
		
		var shuf = [], index;
		
		while(i--) shuf[i] = ar[i];
		ar = [];
						
		while(shuf.length > 0) { 
						
			index = (Math.random() * shuf.length) | 0;
			ar[ar.length] = shuf[index];
			shuf.splice(index, 1);
						
		}
		
		return ar;
		
	}
	
	function enterZoom() {
		
		var $this = $(this),
		off = $this.offset(),
		data = $this.data();
		
		data.x = off.left;
		data.y = off.top;
		
		Jacked.tween(data.zoom, {opacity: 1}, {mode: "timeline"});
		$this.on("mousemove.cj", moveZoom);
		
	}
	
	function moveZoom(event) {
	
		var $this = $(this),
		data = $this.data(),
		
		x = event.pageX - data.x,
		y = event.pageY - data.y,
		
		image = data.image.style,
		zoom = data.zoom.style,
		
		ww = data.ww,
		hh = data.hh;
		
		if(x < 0 || x > data.width || y < 0 || y > data.height) return;
		
		zoom.top = ((y < hh ? 0 : y > data.bufB ? data.snapB : y - hh) | 0) + "px";
		zoom.left = ((x < ww ? 0 : x > data.bufR ? data.snapR : x - ww) | 0) + "px";
		
		image.marginTop = ((-y * 1.5) | 0) + "px";
		image.marginLeft = ((-x * 1.5) | 0) + "px";
		
	}
	
	function leaveZoom() {
		
		var $this = $(this).off("mousemove.cj");
		
		Jacked.tween($this.data("zoom"), {opacity: 0}, {mode: "timeline"});
		
	}
	
	function overShine() {
		
		var $this = $(this).data("fx");
		
		Jacked.stopTween($this[0]);
		
		$this.css("opacity", 1);
		
		Jacked.tween($this[0], {opacity: 0}, {ease: "Sine.easeInOut", mode: "timeline"});
		
	}
	
	function overOutRubix(event) {
		
		var data = $(this).data(), ar = data.fx, i = ar.length, itm, spark,
		tween1 = {opacity: 0}, tween2 = {opacity: 1},
		css = {visibility: "hidden", opacity: 1}, 
		params = {ease: "Quad.easeIn", mode: "timeline"};
		
		if(event.type === "mouseenter") {
		
			data.fx = ar = shuffle(ar, i);
			tween1 = tween2 = {opacity: 0};
		
		}
		else {
		
			tween1 = {opacity: 1};
			tween2 = {opacity: 0};
			
		}
		
		while(i--) {
				
			itm = ar[i];
			spark = itm[1];
			itm = itm[0];
			
			params.delay = i * 20;
			
			Jacked.stopTween(itm[0]);
			Jacked.stopTween(spark[0]);
			
			itm.css("opacity", 1);
			spark.css(css);
			
			Jacked.tween(itm[0], tween1, params);
			Jacked.tween(spark[0], tween2, params);
			
		}
		
	}
	
	function overScramble() {
		
		var ar = $(this).data("fx"), i = ar.length, tween = {}, itm, data;
		
		while(i--) {
			
			itm = ar[i];
			data = itm.data();
			
			tween.left = data.origLeft;
			tween.top = data.origTop;

			Jacked.tween(itm[0], tween, {mode: "timeline"});
			
		}
		
	}
	
	function outScramble() {
		
		var data = $(this).data(), 
		ar = data.fx, 
		i = ar.length,
		cols = data.cols, 
		newW = data.newW, 
		newH = data.newH,
		tween = {},
		col,
		row;
		
		data.fx = ar = shuffle(ar, i);
		
		while(i--) {
			
			col = i % cols;
			row = (i / cols) | 0;
			
			tween.left = newW * col;
			tween.top = newH * row;
			
			Jacked.tween(ar[i][0], tween, {mode: "timeline"});
			
		}
		
	}
	
	function overOutBlinds(event) {
		
		var ar = $(this).data("fx"), 
		i = ar.length, leg = i, itm, square, spark, 
		enter = event.type === "mouseenter",
		params = {ease: "Linear.easeNone", mode: "timeline"},
		tween1 = {opacity: 0}, 
		tween2 = {opacity: 1};
		
		while(i--) {
				
			itm = ar[i];			
			square = itm[1];
			spark = itm[2];
			itm = itm[0];
			
			Jacked.stopTween(itm[0]);
			Jacked.stopTween(spark[0]);
			Jacked.stopTween(square[0]);
			
			itm.css("opacity", 1);
			spark.css("opacity", 1);
			square.css("opacity", 0);
			
			if(enter) {
				
				params.delay = i * 50;
				Jacked.tween(spark[0], tween1, params);
				Jacked.tween(itm[0], tween1, params);
				
			}
			else {
			
				params.delay = (leg - i) * 50;
				Jacked.tween(spark[0], tween1, params);
				Jacked.tween(square[0], tween2, params);
				
			}
			
		}
		
	}
	
	function overOutRotate(event) {
		
		var ar = $(this).data("fx"), i = ar.length, itm,
		params = {duration: 400, ease: "Sine.easeOut", mode: "timeline"},
		tween = {}, callback;
		
		if(event.type === "mouseenter") {
		
			 callback = rotateOn;
			 tween.opacity = 0;
			
		}
		else {
		
			callback = rotateOff;
			tween.opacity = 1;
			
		}
		
		while(i--) {
			
			itm = ar[i];
			Jacked.special(itm[0], {duration: 400, ease: "Sine.easeOut", callback: callback});
			Jacked.tween(itm[1], tween, params);
			
		}
		
	}
	
	function rotateOn(itm, num) {
		
		itm.style[transform] = "rotate(" + (-180 * (1 - num)) + "deg)";
		itm.cjFx.style[transform] = "rotate(" + (180 * num) + "deg)";
		
	}
	
	function rotateOff(itm, num) {
		
		itm.style[transform] = "rotate(" + (-180 * num) + "deg)";
		itm.cjFx.style[transform] = "rotate(" + (180 * (1 - num)) + "deg)";
		
	}
	
	function overOutFlip(event) {
		
		var ar = $(this).data("fx"), i, j, tween = {}, callback, itm,
		params = {duration: 400, ease: "Sine.easeOut", mode: "timeline"};
		
		if(event.type === "mouseenter") {
			
			 var leg = ar.length - 1;
			 callback = flipOn;
			 tween.opacity = 0;
			 i = j = -1; 
			 
			 while(i++ < leg) {
			
				clearTimeout(ar[i].cjTimeout);
				ar[i].cjTimeout = setTimeout(function() {
					
					j++;
					itm = ar[j];
					
					Jacked.special(itm[0], {ease: "Sine.easeOut", callback: callback});
					Jacked.tween(itm[1], tween, params);
				
				}, i * 50);
				
			}
			
		}
		else {
		
			callback = flipOff;
			tween.opacity = 1;
			i = ar.length;
			j = i;
			
			while(i--) {
			
				clearTimeout(ar[i].cjTimeout);
				ar[i].cjTimeout = setTimeout(function() {
					
					j--;
					itm = ar[j];
					
					Jacked.special(itm[0], {ease: "Sine.easeOut", callback: callback});
					Jacked.tween(itm[1], tween, params);
				
				}, i * 50);
				
			}
			
		}
		
	}
	
	function flipOn(itm, num) {
		
		itm.style[transform] = "rotateX(" + (-180 * (1 - num)) + "deg)";
		itm.cjFx.style[transform] = "rotateX(" + (180 * num) + "deg)";
		
	}
	
	function flipOff(itm, num) {
		
		itm.style[transform] = "rotateX(" + (-180 * num) + "deg)";
		itm.cjFx.style[transform] = "rotateX(" + (180 * (1 - num)) + "deg)";
		
	}
	
	function overOutScale(event) {
		
		var ar = $(this).data("fx"), i, j, callback;
		
		if(event.type === "mouseenter") {
			
			var leg = ar.length - 1;
			callback = scaleOn;
			i = j = -1; 
			
			while(i++ < leg) {
			
				clearTimeout(ar[i].cjTimeout);
				ar[i].cjTimeout = setTimeout(function() {
					
					j++;
					Jacked.special(ar[j], {duration: 400, ease: "Sine.easeOut", callback: callback});
				
				}, i * 50);
				
			}
			 
		}
		else {
		
			callback = scaleOff;
			i = ar.length;
			j = i;
			
			while(i--) {
			
				clearTimeout(ar[i].cjTimeout);
				ar[i].cjTimeout = setTimeout(function() {
					
					j--;
					Jacked.special(ar[j], {duration: 400, ease: "Sine.easeOut", callback: callback});
				
				}, i * 50);
				
			}
			
		}
		
	}
	
	function scaleOn(itm, num) {
		
		var num2 = 1 - num;
		
		itm.style[transform] = "scale(" + num + "," + num + ")";
		itm.cjFx.style[transform] = "scale(" + num2 + "," + num2 + ")";
		
	}
	
	function scaleOff(itm, num) {
		
		var num2 = 1 - num;
		
		itm.style[transform] = "scale(" + num2 + "," + num2 + ")";
		itm.cjFx.style[transform] = "scale(" + num + "," + num + ")";
		
	}
	
	function getTransformStyle(itm) {
		
		if("WebkitTransformStyle" in itm) {
	
			return "WebkitTransformStyle";
			
		}
		else if("MozTransformStyle" in itm) {
		
			return "MozTransformStyle";
			
		}
		else if("msTransformStyle" in itm) {
		
			return "msTransformStyle";
			
		}
		else if("transformStyle" in itm) {
			
			return "transformStyle";
			
		}
		
		return null;
		
	}
	
	function getPerspective(itm) {
		
		if("WebkitPerspective" in itm) {
	
			return "WebkitPerspective";
			
		}
		else if("MozPerspective" in itm) {
		
			return "MozPerspective";
			
		}
		else if("msPerspective" in itm) {
		
			return "msPerspective";
			
		}
		else if("perspective" in itm) {
			
			return "perspective";
			
		}
		
		return null;
		
	}
	
	$.fn.jackboxFX = function(type, sets) {
		
		var obj;
		
		if(!sets) {
			
			obj = defaults;
			
		}
		else {
		
			obj = {};
			$.extend(obj, defaults);
			$.extend(obj, sets);
			
		}
		
		if(hasJacked) {
			
			if(!tStyle) {
				
				tStyle = document.body.style;
				perspective = getPerspective(tStyle);
				transformStyle = getTransformStyle(tStyle);
				
			}
				
			return this.each(effect, [type, obj]);
			
		}
		
	};
	
})(jQuery);











