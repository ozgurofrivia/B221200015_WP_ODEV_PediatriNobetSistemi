function loadMusaitlikler() {
    const hocaId = document.getElementById("HocaId").value;
    const tarihContainer = document.getElementById("musaitliklerTarihContainer");
    const saatContainer = document.getElementById("musaitliklerSaatContainer");
    const tarihSelect = document.getElementById("MusaitlikTarihId");
    const saatSelect = document.getElementById("MusaitlikSaatId");

    if (hocaId) {
        fetch(`/Randevu/GetHocaMusaitlikler?hocaId=${hocaId}`)
            .then(response => response.json())
            .then(data => {
                tarihSelect.innerHTML = "<option value=''>Tarih seçin</option>";
                saatSelect.innerHTML = "<option value=''>Saat seçin</option>";

                if (data.length > 0) {
                    tarihContainer.style.display = "block";
                    saatContainer.style.display = "none";

                    const uniqueDates = [...new Set(data.map(item => item.tarih))];

                    uniqueDates.forEach(tarih => {
                        const option = document.createElement("option");
                        option.value = tarih;
                        option.text = new Date(tarih).toLocaleDateString();
                        tarihSelect.appendChild(option);
                    });

                    tarihSelect.addEventListener('change', () => {
                        const selectedDate = tarihSelect.value;
                        saatSelect.innerHTML = "<option value=''>Saat seçin</option>";
                        if (selectedDate) {
                            saatContainer.style.display = "block";
                            const filteredHours = data.filter(item => item.tarih === selectedDate);
                            filteredHours.forEach(item => {
                                const option = document.createElement("option");
                                option.value = item.saat;
                                option.text = item.saat;
                                saatSelect.appendChild(option);
                            });
                        } else {
                            saatContainer.style.display = "none";
                        }
                    });
                } else {
                    tarihContainer.style.display = "none";
                    saatContainer.style.display = "none";
                    alert("Bu hoca için müsaitlik bulunmamaktadır.");
                }
            });
    } else {
        tarihContainer.style.display = "none";
        saatContainer.style.display = "none";
    }
}