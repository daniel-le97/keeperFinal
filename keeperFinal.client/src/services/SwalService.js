import Swal from "sweetalert2";
class SwalsService {
  async undo(url, text) {
    let timerInterval;
    const yes = await Swal.fire({
      title: "Auto close alert!",
      imageUrl: `${url}`,
      position: "top-end",
      imageWidth: 400,
      imageHeight: 200,
      imageAlt: "Custom image",
      showCloseButton: true,
      showCancelButton: true,
      reverseButtons: true,
      showConfirmButton: true,
      focusConfirm: true,
      confirmButtonText: `${text}`,
      confirmButtonAriaLabel: "undo",
      timer: 4000,
      timerProgressBar: true,
      willClose: () => {
        clearInterval(timerInterval);
      },
    });
    return yes.isConfirmed
  }
}
export const swalsService = new SwalsService();
