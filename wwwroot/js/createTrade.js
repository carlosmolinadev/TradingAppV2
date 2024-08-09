   document.addEventListener('alpine:init', () => {
      Alpine.data('createTrade', () => {
         return {
            price: 200,
            quantity: 0,
            amount: 0,
            symbolModal: false,
            setQuantity() {
               if (!this.price) {
                  return;
               }
               this.quantity = this.amount * this.price;
            },
            setAmount() {
               if (!this.price) {
                  return;
               }
            },
            symbolModalHandler() {
               this.symbolModal = !this.symbolModal
               console.log(this.symbolModal);
            },
            clearValidationError(element) {
               const div = document.createElement('div');
               div.id = 'validation-amount';
               const subling = element.nextElementSibling;
               subling.remove();
               element.insertAdjacentElement('afterend', div);

            }
         }
      })
   });