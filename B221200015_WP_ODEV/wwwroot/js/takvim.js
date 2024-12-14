document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth', // Varsayılan görünüm
        headerToolbar: {
            left: 'prev,next today', // Geri, İleri ve Bugün butonları
            center: 'title', // Başlık (ör. Aralık 2024)
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek' // Görünümler
        },
        buttonText: {
            today: 'Bugün',         // "Today" yerine "Bugün"
            month: 'Ay',           // "Month" yerine "Ay"
            week: 'Hafta',         // "Week" yerine "Hafta"
            day: 'Gün',            // "Day" yerine "Gün"
            list: 'Ajanda'         // "List" yerine "Ajanda"
        },
        locale: 'tr', // Türkçe dil desteği
        views: {
            timeGridDay: {
                titleFormat: { year: 'numeric', month: 'long', day: 'numeric', weekday: 'long' },
                slotDuration: '00:30',
                slotLabelFormat: { hour: 'numeric', minute: '2-digit', hour12: false }
            },
            listWeek: {
                titleFormat: { year: 'numeric', month: 'long', day: 'numeric' },
                dayHeaderFormat: { weekday: 'long' }
            }
        },
        events: '/Takvim/GetEvents', // Etkinliklerin çekileceği yer
        editable: true, // Sürükle bırak özelliği
        selectable: true, // Tarih seçilebilir
        nowIndicator: true,
        height: 'auto',
        themeSystem: 'standard',

        // Event başlıklarını düzenlemek için CSS ekle
        eventDidMount: function (info) {
            info.el.style.whiteSpace = 'normal'; // Satır geçişine izin ver
            info.el.style.wordWrap = 'break-word'; // Uzun kelimeleri böl
            info.el.style.lineHeight = '1.2'; // Satır yüksekliğini optimize et
        }
    });

    calendar.render();
});
