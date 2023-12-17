var models = [
    {
         image: 'images/images.jpeg',

    },
    {
        image: 'images/images2.webp',

    },
    {
        image: 'images/images3.jpeg',

    },
    {
        image: 'images/images4.webp',

    },
    {
        image: 'images/images5.jpeg',

    }

];

var index = 0;
var slaytCount = models.length;
var interval;

var settings = {
    duration: '2000',
    random: false
};

init(settings);


document.querySelectorAll('.arrow').forEach(function (item) {
    item.addEventListener('mouseenter', function () {
        clearInterval(interval);
    })
});

document.querySelectorAll('.arrow').forEach(function (item) {
    item.addEventListener('mouseleave', function () {
        init(settings);
    })
})


function init(settings) {

    var prev;
    interval = setInterval(function () {

        if (settings.random) {

            do {
                index = Math.floor(Math.random() * slaytCount);
            } while (index == prev)
            prev = index;
        } else {

            if (slaytCount == index + 1) {
                index = -1;
            }
            showSlide(index);
            console.log(index);
            index++;
        }
        showSlide(index);

    }, settings.duration)


}



function showSlide(i) {

    index = i;

    if (i < 0) {
        index = slaytCount - 1;
    }
    if (i >= slaytCount) {
        index = 0;
    }

    document.querySelector('.slider-img-top').setAttribute('src', models[index].image);


}
