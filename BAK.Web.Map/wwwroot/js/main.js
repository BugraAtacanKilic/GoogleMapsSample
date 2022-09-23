var _Map, _Draw, _Source, _Layer;

InitializeMap = () => {

	_Source = new ol.source.Vector({
		wrapX: false
	});

	_Layer = new ol.layer.Vector({
		source: _Source,
	});

	_Map = new ol.Map({
		target: 'map',
		layers: [
			new ol.layer.Tile({
				source: new ol.source.OSM()
			}),
			_Layer
		],
		view: new ol.View({
			center: [3875337.27, 4673762.79],
			zoom: 7
		})
	});
}

QueryPoint = () => {

}

AddInteraction = () => {
	_Draw = new ol.interaction.Draw({
		source: _Source,
		type: "Point"
	});

	_Map.addInteraction(_Draw);

	_Draw.setActive(false);

	_Draw.on(
		"drawend",
		(_event) => {
			$.jsPanel({
				paneltype: 'modal',
				headerTitle: 'Point Information',
				theme: 'success',
				show: 'animated fadeInDownBig',
				content: '<div class="jsPanel-content jsPanel-content-nofooter" style="border-top:1px solid #fff;width:100%;height:200px;overflow:hidden"><div class="input-group" style="padding:30px 40px 10px 40px"><input type="text" class="form-control" placeholder="Name" id="Name" aria-describedby="sizing-addon2"></div><div class="input-group" style="padding:10px 40px 10px 40px"><input type="text" class="form-control" placeholder="Number" id="Number" aria-describedby="sizing-addon2"></div><p style="text-align:center;padding-top:10px"><button class="btn btn-default" type="button">Submit</button></p></div>',
				callback: function (panel) {
					$("input:first", this).focus();
					$("button", this.content).click(function () {
						
						var res = _event.feature.getGeometry().getCoordinates();
						var lat = res[0];
						var lon = res[1];
						var MapModel = { Points:[ { "Name": document.querySelector('#Name').value, "Number": document.querySelector('#Number').value, "Lat": lat, "Lon": lon  } ]};
						console.log(JSON.stringify(MapModel));
						$.ajax({
							url: "/DataWriter",
							type: "POST",
							data: JSON.stringify(MapModel),
							contentType: "application/json"
						});

						panel.close()
					});
				}
			});
			//console.log(_event.feature.getGeometry().getCoordinates());
		});
	_Draw.setActive(false);
}

AddPoint = () => {

	_Draw.setActive(true);
}