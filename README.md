# ASP.NET MVC5

ASP.NET MVC5 là một framework mạnh mẽ được phát triển bởi Microsoft, giúp các nhà phát triển xây dựng các ứng dụng web trên nền tảng .NET với kiến trúc MVC (Model-View-Controller). Framework này cung cấp khả năng kiểm soát toàn diện đối với HTML, CSS, và JavaScript, đồng thời hỗ trợ các tiêu chuẩn web hiện đại.

![ASP.NET MVC](https://tiennampham.wordpress.com/wp-content/uploads/2019/02/asp-net-mvc.jpg?w=640)

## Mô hình MVC là gì?

Mô hình MVC giúp tách biệt các thành phần chính trong một ứng dụng web thành ba phần:

- **Model**: Chịu trách nhiệm quản lý logic nghiệp vụ và dữ liệu của ứng dụng.
- **View**: Chịu trách nhiệm hiển thị dữ liệu và tương tác với người dùng.
- **Controller**: Điều phối yêu cầu từ người dùng, thao tác dữ liệu và quyết định dữ liệu nào sẽ được hiển thị trong View.

### Sơ đồ MVC

![Sơ đồ MVC](https://upload.wikimedia.org/wikipedia/commons/a/a0/MVC-Process.svg)

## Tại sao nên sử dụng ASP.NET MVC5?

1. **Phân tách trách nhiệm**: MVC giúp tách biệt rõ ràng giữa logic nghiệp vụ, giao diện người dùng và điều khiển.
2. **Kiểm soát tốt hơn**: Bạn có thể kiểm soát hoàn toàn HTML, CSS, và JavaScript, giúp tối ưu giao diện người dùng.
3. **Hỗ trợ tiêu chuẩn web hiện đại**: ASP.NET MVC5 tích hợp sẵn HTML5, CSS3 và hỗ trợ tốt cho AJAX.
4. **Routing linh hoạt**: Cung cấp cơ chế routing thân thiện với URL giúp xây dựng các địa chỉ web dễ đọc, dễ nhớ.
5. **Tích hợp với hệ sinh thái ASP.NET**: MVC5 dễ dàng tích hợp với Web API, SignalR, và nhiều công nghệ khác trong hệ sinh thái ASP.NET.

## Các tính năng nổi bật của ASP.NET MVC5

- **Attribute Routing**: Định nghĩa routing thông qua các thuộc tính (attributes) ngay trên các action hoặc controller.
- **Authentication Filters**: Cho phép thực hiện xác thực người dùng trước khi controller hoặc action được thực thi.
- **ASP.NET Identity**: Hệ thống quản lý người dùng mới hỗ trợ OAuth và OpenID.
- **Tích hợp Bootstrap**: Dễ dàng thiết kế giao diện người dùng responsive và hiện đại với Bootstrap.
- **One ASP.NET**: Hỗ trợ kết hợp các loại dự án web như WebForms, MVC, và Web API trong cùng một dự án.

## Cài đặt và bắt đầu

### Yêu cầu hệ thống

- **.NET Framework 4.5** hoặc mới hơn
- **Visual Studio 2013** hoặc mới hơn
- **SQL Server Express** hoặc SQL Server phiên bản đầy đủ

### Cài đặt

1. Tải và cài đặt [Visual Studio](https://visualstudio.microsoft.com/).
2. Tạo một dự án mới bằng cách chọn **New Project** > **ASP.NET Web Application** > **MVC**.
   ![Tạo dự án mới](https://docs.microsoft.com/en-us/aspnet/core/includes/web/create-web-app-vs/_static/new-web-application.png)
3. Chọn các tùy chọn cấu hình ban đầu cho dự án, bao gồm **Authentication** và các thành phần khác như **Web API** nếu cần.
   ![Chọn template MVC](https://docs.microsoft.com/en-us/aspnet/core/includes/web/create-web-app-vs/_static/mvc-template.png)

### Cấu trúc dự án

Khi tạo dự án ASP.NET MVC5, cấu trúc thư mục sẽ bao gồm:

- **Controllers**: Chứa các lớp controller (ví dụ: `HomeController.cs`), quản lý các yêu cầu từ người dùng.
- **Models**: Chứa các lớp mô hình dữ liệu (ví dụ: `Product.cs`), quản lý dữ liệu và logic nghiệp vụ.
- **Views**: Chứa các tệp giao diện người dùng (ví dụ: `Index.cshtml`), định nghĩa cách dữ liệu được hiển thị.
- **Content**: Chứa các tài nguyên tĩnh như CSS, hình ảnh.
- **Scripts**: Chứa các tệp JavaScript (ví dụ: `jquery.js`).
