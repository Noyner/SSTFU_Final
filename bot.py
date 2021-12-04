import telebot
from telebot.types import Message, ReplyKeyboardMarkup, KeyboardButton, InlineKeyboardMarkup, InlineKeyboardButton, CallbackQuery

API_TOKEN = '5009344347:AAFvCPBVGNXz_cJFNJzUGeHTPGRSJYPPP0E'
bot = telebot.TeleBot(API_TOKEN)


CHECK_EMPTY_PLACES = "Проверить наличие свободных мест"


@bot.message_handler(commands=['start'], chat_types=['private'])
def test(message: Message):
    markup = ReplyKeyboardMarkup()
    markup.add(KeyboardButton(CHECK_EMPTY_PLACES))
    bot.send_message(message.chat.id, "Привет " + message.from_user.first_name, reply_markup=markup)


@bot.message_handler(content_types=['text'], chat_types=['private'])
def test(message: Message):
    if message.text == CHECK_EMPTY_PLACES:
        markup = ReplyKeyboardMarkup()
        markup.add(KeyboardButton("Отправить свою геопозицию", request_location=True))
        markup.add(KeyboardButton("Выбрать из ранее заполненных"))
        bot.send_message(message.chat.id, "Отправьте локацию для проверки свободных мест", reply_markup=markup)
    if message.text == "Выбрать из ранее заполненных":
        markup = InlineKeyboardMarkup(row_width=2)
        markup.add(InlineKeyboardButton("asddsa", callback_data="dsa"))
        bot.send_message(message.chat.id, "Отправьте локацию для проверки свободных мест", reply_markup=markup)

    # bot.send_message(message.chat.id, message.text)


@bot.message_handler(content_types=['location'], chat_types=['private'])
def test(message: Message):
    check_empty_places(message)


# @bot.callback_query_handler(func=lambda call: call.data in ['good', 'bad', 'good1', 'bad1'])
@bot.callback_query_handler(func=lambda call: True)
def callback_handler(call: CallbackQuery):
    check_empty_places(call.message)
    bot.answer_callback_query(callback_query_id=call.id, show_alert=False)


def check_empty_places(message: Message):
    bot.send_message(message.chat.id, "Свободных мест рядом: 10")
    return 0


bot.polling(none_stop=True)
