export function loadMap() {
    mapboxgl.accessToken = 'pk.eyJ1IjoidHN1bmRlcmUiLCJhIjoiY2s2czZ6ZmFvMGI2bjNmcDk5YXR0Y2p4eSJ9.J-Ua0s1JO3t8RFP8mmYZzA';
    const map = new mapboxgl.Map({
        container: 'map', // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: 'mapbox://styles/mapbox/outdoors-v12', // style URL mapbox://styles/mapbox/outdoors-v12
        center: [-1.7411, 46.84914953388066], // starting position [lng, lat]
        zoom: 10
    });

    // Domaine de la Bernerie    
    const popupBernerie = new mapboxgl.Popup({ offset: 35, closeButton: false }).setText('Domaine de la Bernerie');
    const elementBernerie = document.createElement('div');
    elementBernerie.id = 'markerBernerie';
    const markerBernerie = new mapboxgl.Marker({ color: "#DC143C" }).setLngLat([-1.7411, 46.84914953388066]).setPopup(popupBernerie).addTo(map);
    markerBernerie.togglePopup();

    // Gite de l'Hubertiere
    const popupHubertiere = new mapboxgl.Popup({ offset: 35, closeButton: false }).setText('Gîte de l\'Hubertiere');
    const elementHubertiere = document.createElement('div');
    elementHubertiere.id = 'markerHubertiere';
    const markerHubertiere = new mapboxgl.Marker({ color: "#bc6c25" }).setLngLat([-1.6880435571812855, 46.82575275601642]).setPopup(popupHubertiere).addTo(map);
    markerHubertiere.togglePopup();

    // Gite de la Charrie
    const popupCharrie = new mapboxgl.Popup({ offset: 35, closeButton: false }).setText('Gite de la Charrie');
    const elementCharrie = document.createElement('div');
    elementCharrie.id = 'markerCharrie';
    const markerCharrie = new mapboxgl.Marker({ color: "#bc6c25" }).setLngLat([-1.747576653853278, 46.866937776056105]).setPopup(popupCharrie).addTo(map);
    markerCharrie.togglePopup();

    // Camping Paradis
    const popupCamping = new mapboxgl.Popup({ offset: 35, closeButton: false }).setText('Camping Paradis');
    const elementCamping = document.createElement('div');
    elementCamping.id = 'markerCamping';
    const markerCamping = new mapboxgl.Marker({ color: "#bc6c25" }).setLngLat([-1.7759067140242604, 46.81551845894582]).setPopup(popupCamping).addTo(map);
    markerCamping.togglePopup(); 
}  