$(document).ready(function () {
    // Activate modals with Bootstrap
    $('.course-card img').on('click', function () {
        $('#' + $(this).data('modal')).modal('show');
    });

    // Trainer Slider
    let currentHvlIndex = 0;
    const hvlVisible = 3;
    const hvlTotal = $('.hvl-slides .col-md-4').length;

    function showHvl(index) {
        const offset = index * (100 / hvlVisible);
        $('.hvl-slides').css('transform', `translateX(-${offset}%)`);
    }

    function nextTrainer() {
        currentHvlIndex += hvlVisible;
        if (currentHvlIndex >= hvlTotal) currentHvlIndex = 0;
        showHvl(currentHvlIndex);
    }

    function prevTrainer() {
        currentHvlIndex -= hvlVisible;
        if (currentHvlIndex < 0) currentHvlIndex = hvlTotal - hvlVisible;
        showHvl(currentHvlIndex);
    }

    $('.next-trainer').on('click', nextTrainer);
    $('.prev-trainer').on('click', prevTrainer);
    setInterval(nextTrainer, 5000);

    // Countdown Timer
    function startCountdown() {
        const countDownDate = new Date("Jul 13, 2025 23:59:59").getTime();
        const timer = setInterval(() => {
            const now = new Date().getTime();
            const distance = countDownDate - now;
            const days = Math.floor(distance / (1000 * 60 * 60 * 24));
            const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((distance % (1000 * 60)) / 1000);
            $('#timer').text(`${days}d ${hours}h ${minutes}m ${seconds}s`);
            if (distance < 0) {
                clearInterval(timer);
                $('#timer').text("Hết hạn");
            }
        }, 1000);
    }
    startCountdown();
});